using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }
   

    public StateObjectBase CurrentState;

    protected PlayerMaster playerMasterComp;
    protected StateInitialiser stateInitialiserComp;

    protected Observer<List<CardSuite>> suiteObserver;
    protected Observer<CardBase> CardPlayObserver;
    protected Observer<CardHolder> TurnOverObserver;

    protected CardHolder currentHolder;

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
        playerMasterComp = GetComponent<PlayerMaster>();
        stateInitialiserComp = GetComponent<StateInitialiser>();

       
    }

    private void Start()
    {
        var playerArea = FindObjectOfType<PlayerArea>();
        if (!playerArea) { return; }
       

        SetupPlayerArea(playerArea);

        BindPlayerHandObservers();

        SetupPlayerTurn();

        var aiArea = FindObjectOfType<AIArea>();
        if (!aiArea) { return; }
        TurnOverObserver = new Observer<CardHolder>(CheckShouldStateTransition);
        aiArea.OnTurnEnd.AddObserver(TurnOverObserver);
        playerArea.OnTurnEnd.AddObserver(TurnOverObserver);
        playerMasterComp.OnCheckTurnOver.AddObserver(TurnOverObserver);

        currentHolder = stateInitialiserComp.IdHoldersPair[CurrentState.HolderId];
    }


    private void SetupPlayerArea(PlayerArea playerArea)
    {
    
     

        BindSuiteObserver(playerArea);
        playerMasterComp.AddPlayerStartCard(playerArea);
       
    }

    private void BindPlayerHandObservers()
    {
        var playerHand = FindObjectOfType<CardHand>();
        if (!playerHand) { return; }
        CardPlayObserver = new Observer<CardBase>(playerMasterComp.UpdateCardAllowence);
        playerHand.OnCardPlayed.AddObserver(CardPlayObserver);
    }

    private void BindSuiteObserver(PlayerArea playerArea)
    {
        suiteObserver = new Observer<List<CardSuite>>(playerMasterComp.UpdateUnavialableSuites);
        playerArea.OnUpdateAvailableSuites.AddObserver(suiteObserver);
    }

    private void CheckShouldStateTransition(CardHolder sender)
    {
       
        if (!CurrentState) { return; }

        if (!sender) { sender = currentHolder; }

        StateObjectBase PotentialNewState = CheckStateLinks(sender);

        if (PotentialNewState == null) { return; }
        if (PotentialNewState == CurrentState) { return; }

        CurrentState.Exit(this, currentHolder);
        CurrentState = PotentialNewState;
        currentHolder = stateInitialiserComp.IdHoldersPair[CurrentState.HolderId];
        CurrentState.Enter(this, currentHolder);

        Debug.Log(CurrentState.name);
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
        if (playerMasterComp) { retval = playerMasterComp.HeroPlayedThisTurn; }
        return retval;
    }

    public List<CardSuite> GetActionCardAllowence()
    {
        var retval = new List<CardSuite>();
        if (playerMasterComp) { retval = playerMasterComp.ActionCardAllowence; }
        return retval;
    }

    public List<CardSuite> GetUnavailableSuites()
    {
        var retval = new List<CardSuite>();
        if (playerMasterComp) { retval = playerMasterComp.UnavailableSuites; }
        return retval;
    }

    public void SetupPlayerTurn()
    {
        playerMasterComp.SetupCardAllowence();
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
