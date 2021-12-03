using UnityEngine;
using UnityEngine.Events;

public class CardBase : MonoBehaviour
{
    public Vector3 SlotedPosition { get; set; }
    public Vector3 OriginalSize { get; private set; } = new Vector3(1f, 1f, 1f);

    public UnityEvent<CardBase, Vector3, Vector3> HighLightedEvent = new UnityEvent<CardBase, Vector3, Vector3>();

    protected string cardName;
    protected int cardValue;
    protected CardSuite cardSuite;

    protected CardAction[] playActions;
    protected CardAction[] drawActions;
    protected CardAction[] leaveActions;


    private void Start()
    {

    }

    public virtual void Setup(CardStatsBase cardStats)
    {
        cardName = cardStats.DisplayName;
        cardValue = cardStats.Value;
        cardSuite = cardStats.suite;
        playActions = cardStats.PlayActions;
        drawActions = cardStats.DrawActions;
        leaveActions = cardStats.LeaveActions;
    }

    public void Play()
    {
        //TODO probably needs a destination slot to place it at
        TryDoPlayActions();
    }

    public void Select()
    {

    }

    public void Remove()
    {
        TryDoLeaveActions();
    }

    public void TryDoPlayActions()
    {
        TryDoActions(playActions);
    }

    public void TryDoDrawActions()
    {
        TryDoActions(drawActions);
    }

    public void TryDoLeaveActions()
    {
        TryDoActions(leaveActions);
    }

    protected void TryDoActions(ICardAction[] actionsToDo)
    {
        foreach (var action in actionsToDo)
        {
            action.DoCardAction();
        }
    }

    private void OnMouseEnter()
    {
        SetHighlightThisCard(this, new Vector3(0f, 0f, 3f), new Vector3(0.2f, 0.2f, 0.2f));
    }



    private void OnMouseExit()
    {
        SetHighlightThisCard(null, Vector3.zero, Vector3.zero);
    }

    private void SetHighlightThisCard(CardBase card, Vector3 posOffset, Vector3 scaleToSet)
    {
        if (!transform.parent) { return; }

        var hand = transform.parent.GetComponent<CardHand>();
        if (!hand) { return; }
        if (hand.IsThereACardSelection()) { return; }

        HighLightedEvent.Invoke(card, SlotedPosition + posOffset, OriginalSize + scaleToSet);

    }
}
