using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }

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
    protected Observer<CardHand> TurnOverObserver;

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
        var cardHand = FindObjectOfType<CardHand>();
        if (!cardHand) { return; }
        CardPlayObserver = new Observer<CardBase>(UpdateCardAllowence);
        cardHand.OnCardPlayed.AddObserver(CardPlayObserver);

        TurnOverObserver = new Observer<CardHand>(CheckIfPlayerTurnOver);
        cardHand.OnCheckTurnOver.AddObserver(TurnOverObserver);

    }

    private void BindSuiteObserver(PlayArea playerArea)
    {
        suiteObserver = new Observer<List<CardSuite>>(UpdateUnavialableSuites);
        playerArea.OnUpdateAvailableSuites.AddObserver(suiteObserver);
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

    private void CheckIfPlayerTurnOver(CardHand hand)
    {
        //Also should probably make call if all enemies are dead on the oposite board
        var cantPlayHeroCard = HeroPlayedThisTurn || !hand.DoesHeldCardsContainType(CardType.Hero);
        if (ActionCardAllowence.Count == 0 && cantPlayHeroCard)
        {
            //Player turn is over, transition to ai turn
            Debug.Log("Player turn over");
        }
        //TODO use state machine to siwthc state to enemy turn
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
