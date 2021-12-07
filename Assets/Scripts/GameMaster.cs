using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }

    public StateObjectBase CurrentState;

    public bool HeroPlayedThisTurn { get; private set; } = false;

    public List<CardSuite> UnavailableSuites { get; private set; } = new List<CardSuite>() {
        CardSuite.Spades,
        CardSuite.Diamonds,
        CardSuite.Hearts };

    public List<CardSuite> ActionCardAllowence { get; private set; } = new List<CardSuite>() {
         CardSuite.Clubs,
        CardSuite.Spades,
        CardSuite.Diamonds,
        CardSuite.Hearts };

    protected CardSuite[] cardSuitesInGame = new CardSuite[] {
        CardSuite.Clubs,
        CardSuite.Spades,
        CardSuite.Diamonds,
        CardSuite.Hearts };

    protected Observer<List<CardSuite>> suiteObserver;
    protected Observer<CardBase> CardPlayObserver;
    protected Observer<CardHolder> TurnOverObserver;

    protected CardHand playerHand;
    protected CardHolder AiBoard;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

    }

    private void Start()
    {
        SetupPlayerArea();

        BindHandObservers();

        SetupCardAllowence();
    }

    public void UpdateCardAllowence(CardBase cardToRemove)
    {
        if (cardToRemove.Type == CardType.Hero)
        {
            HeroPlayedThisTurn = true;
            return;
        }

        var suite = cardToRemove.Suite;

        if (ActionCardAllowence.Contains(suite))
        {
            ActionCardAllowence.Remove(suite);
        }
    }

    private void SetupCardAllowence()
    {
        ActionCardAllowence = cardSuitesInGame.ToList();
        foreach (var suite in UnavailableSuites)
        {
            ActionCardAllowence.Remove(suite);
        }
        HeroPlayedThisTurn = false;
    }

    private void SetupPlayerArea()
    {
        var playAreas = FindObjectsOfType<PlayArea>();
        var playerArea = playAreas.FirstOrDefault(q => q.Owner == OwnerEnum.Player);
        if (!playerArea) { return; }

        BindSuiteObserver(playerArea);
        AddPlayerStartCard(playerArea);
    }

    private void BindHandObservers()
    {
        playerHand = FindObjectOfType<CardHand>();
        if (!playerHand) { return; }
        CardPlayObserver = new Observer<CardBase>(UpdateCardAllowence);
        playerHand.OnCardPlayed.AddObserver(CardPlayObserver);

        TurnOverObserver = new Observer<CardHolder>(CheckShouldStateTransition);
        playerHand.OnCheckTurnOver.AddObserver(TurnOverObserver);

    }

    private void BindSuiteObserver(PlayArea playerArea)
    {
        suiteObserver = new Observer<List<CardSuite>>(UpdateUnavialableSuites);
        playerArea.OnUpdateAvailableSuites.AddObserver(suiteObserver);
    }

    private void AddPlayerStartCard(PlayArea playerArea)
    {
        var playerDeck = FindObjectOfType<PlayerCardDeck>();
        if (!playerDeck) { return; }

        var startingHero = playerDeck.AvilableCards.CardStats.FirstOrDefault(
            q => q.cardType == CardType.Hero &&
            q.suite == CardSuite.Clubs);
        if (!startingHero) { return; }

        var firstCard = playerDeck.CreateCardBasedOnStats(startingHero);
        playerArea.AddCard(firstCard);
    }

    private void UpdateUnavialableSuites(List<CardSuite> suitesInArea)
    {
        var tempSuites = cardSuitesInGame.ToList();

        foreach (var suite in suitesInArea)
        {
            tempSuites.Remove(suite);
        }
        UnavailableSuites = tempSuites;
    }

    private void CheckShouldStateTransition(CardHolder holder)
    {
        if (!CurrentState) { return; }

        StateObjectBase PotentialNewState = null;
        foreach (var link in CurrentState.GetStateLinks())
        {
            if (link.LinkConditions.Length == 0)
            {
                PotentialNewState = link.LinkedState;
                break;
            }

            var correctConditions = 0;
            foreach (var condition in link.LinkConditions)
            {
                if (condition.CheckCondition(this, holder)) { correctConditions++; }
            }

            if (correctConditions == link.LinkConditions.Length)
            {
                PotentialNewState = link.LinkedState;
                break;
            }
        }

        if (PotentialNewState == null) { return; }
        CurrentState.Exit(this, holder);
        CurrentState = PotentialNewState;
       
        CardHolder newHolder = null;
        if (holder == playerHand)
        { newHolder = AiBoard; }
        else if (holder == AiBoard) 
        { newHolder = playerHand; }

        CurrentState.Enter(this, newHolder);
    }

    //TODO use Observer pattern to look at the play areas and the player hand.
    //use state pattern to deal with whos turn it is.
    //If player cant play any more cards, transition AI turn.
    //If AI turn is done, go back to player turn.
    //If player area is empty fo cards the player loses the game
    //If enemy area is empty then the player won a round and can continue to the next round.
    // if the player plays a hero card to tehir play area,
    // add its suite to the available suites for the players action card draw.
}
