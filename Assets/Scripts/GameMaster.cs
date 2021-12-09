using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }

    public Text RoundText;

    public RectTransform NextRoundUI;
    public RectTransform RestartUI;

    protected PlayerMaster playerMasterComp;
    protected StateMachineComp stateMachineComp;

    protected Observer<List<CardSuite>> suiteObserver;
    protected Observer<CardBase> cardPlayObserver;
    protected Observer<CardHolder> turnOverObserver;

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
        stateMachineComp = GetComponent<StateMachineComp>();
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
        stateMachineComp.CurrentState.Enter(this, null);
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
        turnOverObserver = new Observer<CardHolder>(stateMachineComp.CheckShouldStateTransition);
        aIArea.OnTurnEnd.AddObserver(turnOverObserver);
        playerArea.OnTurnEnd.AddObserver(turnOverObserver);
        playerMasterComp.OnCheckTurnOver.AddObserver(turnOverObserver);
    }

    //setsup the correct menu depending on who lost the round
    public void RoundOver()
    {
        if (stateMachineComp.CurrentState.HolderId == 0)
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
        stateMachineComp.CheckShouldStateTransition(aIArea);
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
