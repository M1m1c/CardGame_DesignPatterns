using UnityEngine;
using UnityEngine.Events;

public class CardBase : MonoBehaviour
{
    public Vector3 SlotedPosition { get; set; }

    public UnityEvent<CardBase> HighLightedEvent = new UnityEvent<CardBase>();
    
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
        if (!transform.parent) { return; }
        var hand = transform.parent.GetComponent<CardHand>();
        if (hand)
        {
            HighLightedEvent.Invoke(this);
            transform.position = SlotedPosition + new Vector3(0f, 0f, 3f);
            transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
    }

    private void OnMouseExit()
    {
        if (!transform.parent) { return; }
        var hand = transform.parent.GetComponent<CardHand>();
        if (hand)
        {
            HighLightedEvent.Invoke(null);
            transform.position = SlotedPosition;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

}
