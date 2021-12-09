using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }


    public StateObjectBase CurrentState;

    public Text RoundText;

    public RectTransform NextRoundUI;
    public RectTransform RestartUI;

    protected PlayerMaster playerMasterComp;
    protected StateInitialiser stateInitialiserComp;

    protected Observer<List<CardSuite>> suiteObserver;
    protected Observer<CardBase> cardPlayObserver;
    protected Observer<CardHolder> turnOverObserver;

    protected CardHolder currentHolder;

    protected PlayerArea playerArea;
    protected AIArea aIArea;

    protected int roundCount = 0;

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
        playerArea = FindObjectOfType<PlayerArea>();
        if (!playerArea) { return; }
        aIArea = FindObjectOfType<AIArea>();
        if (!aIArea) { return; }

        BindSuiteObserver(playerArea);
        playerMasterComp.AddPlayerStartCard(playerArea);

        BindPlayerHandObservers();
        BindTurnOverObserver();
        SetupPlayerTurn();
        CurrentState.Enter(this, null);
    }

    private void BindSuiteObserver(PlayerArea playerArea)
    {
        suiteObserver = new Observer<List<CardSuite>>(playerMasterComp.UpdateUnavialableSuites);
        playerArea.OnUpdateAvailableSuites.AddObserver(suiteObserver);
    }

    private void BindPlayerHandObservers()
    {
        var playerHand = FindObjectOfType<CardHand>();
        if (!playerHand) { return; }
        cardPlayObserver = new Observer<CardBase>(playerMasterComp.UpdateCardAllowence);
        playerHand.OnCardPlayed.AddObserver(cardPlayObserver);
    }

    private void BindTurnOverObserver()
    {
        turnOverObserver = new Observer<CardHolder>(CheckShouldStateTransition);
        aIArea.OnTurnEnd.AddObserver(turnOverObserver);
        playerArea.OnTurnEnd.AddObserver(turnOverObserver);
        playerMasterComp.OnCheckTurnOver.AddObserver(turnOverObserver);
    }

    //gets a reference to a new state if it is viable, then swithces to it
    private void CheckShouldStateTransition(CardHolder sender)
    {

        if (!CurrentState) { return; }

        if (!sender) { sender = currentHolder; }

        StateObjectBase PotentialNewState = CheckStateLinks(sender);

        if (PotentialNewState == null) { return; }
        if (PotentialNewState == CurrentState) { return; }

        SwitchState(PotentialNewState);
    }

    //Checks the link condition of each of this states links using the sender as the target of those checks
    //returns a the state a link leads to if the conditions are all true
    private StateObjectBase CheckStateLinks(CardHolder sender)
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
                if (condition.CheckCondition(this, sender)) { correctConditions++; }
            }

            if (correctConditions == link.LinkConditions.Length)
            {
                PotentialNewState = link.LinkedState;
                break;
            }
        }

        return PotentialNewState;
    }

    //exits the current state and enters a new state
    private void SwitchState(StateObjectBase PotentialNewState)
    {
        CurrentState.Exit(this, currentHolder);
        CurrentState = PotentialNewState;
        currentHolder = stateInitialiserComp.IdHoldersPair[CurrentState.HolderId];
        CurrentState.Enter(this, currentHolder);
    }



    //setsup the correct menu depending on who lost the round
    public void RoundOver()
    {
        if (CurrentState.HolderId == 0)
        {
            if (playerArea.GetCurrentHeldCards() <= 0)
            {
                if (RestartUI) { RestartUI.gameObject.SetActive(true); }
            }
            else if (aIArea.GetCurrentHeldCards() <= 0)
            {
                roundCount++;
                if (RoundText) { RoundText.text = "" + roundCount; }
                if (NextRoundUI) { NextRoundUI.gameObject.SetActive(true); }
            }
        }
    }

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void GoToNextRound()
    {
        if (NextRoundUI) { NextRoundUI.gameObject.SetActive(false); }
        CheckShouldStateTransition(aIArea);
        aIArea.AddUnitsToArea();
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
        playerMasterComp.PlayerDrawCards();
    }
}
