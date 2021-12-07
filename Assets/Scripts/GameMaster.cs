using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }
   

    public StateObjectBase CurrentState;

    protected PlayerMaster PlayerMasterComp;

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
        PlayerMasterComp = GetComponent<PlayerMaster>();
    }

    private void Start()
    {
        SetupPlayerArea();

        BindPlayerHandObservers();

        PlayerMasterComp.SetupCardAllowence();
    }

   
    private void SetupPlayerArea()
    {
        var playerArea = FindObjectOfType<PlayerArea>();
        if (!playerArea) { return; }

        BindSuiteObserver(playerArea);
        PlayerMasterComp.AddPlayerStartCard(playerArea);
    }

    private void BindPlayerHandObservers()
    {
        playerHand = FindObjectOfType<CardHand>();
        if (!playerHand) { return; }
        CardPlayObserver = new Observer<CardBase>(PlayerMasterComp.UpdateCardAllowence);
        playerHand.OnCardPlayed.AddObserver(CardPlayObserver);

        TurnOverObserver = new Observer<CardHolder>(CheckShouldStateTransition);
        playerHand.OnCheckTurnOver.AddObserver(TurnOverObserver);

    }

    private void BindSuiteObserver(PlayerArea playerArea)
    {
        suiteObserver = new Observer<List<CardSuite>>(PlayerMasterComp.UpdateUnavialableSuites);
        playerArea.OnUpdateAvailableSuites.AddObserver(suiteObserver);
    }

    private void CheckShouldStateTransition(CardHolder holder)
    {
        if (!CurrentState) { return; }

        StateObjectBase PotentialNewState = CheckStateLinks(holder);

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

    private StateObjectBase CheckStateLinks(CardHolder holder)
    {
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

        return PotentialNewState;
    }

    public bool IsHeroPlayedThisTurn()
    {
        var retval = false;
        if (PlayerMasterComp) { retval = PlayerMasterComp.HeroPlayedThisTurn; }
        return retval;
    }

    public List<CardSuite> GetActionCardAllowence()
    {
        var retval = new List<CardSuite>();
        if (PlayerMasterComp) { retval = PlayerMasterComp.ActionCardAllowence; }
        return retval;
    }

    public List<CardSuite> GetUnavailableSuites()
    {
        var retval = new List<CardSuite>();
        if (PlayerMasterComp) { retval = PlayerMasterComp.UnavailableSuites; }
        return retval;
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
