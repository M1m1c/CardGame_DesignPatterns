using UnityEngine;
using UnityEngine.Events;

public class CardBase : MonoBehaviour
{
    public Vector3 SlotedPosition { get; set; }
    public Vector3 OriginalSize { get; private set; } = new Vector3(1f, 1f, 1f);
    public int CardValue { get { return cardValue; } }
    public OwnerEnum Owner { get { return owner; } }

    protected string cardName;
    protected int cardValue;
    protected CardSuite cardSuite;
    protected OwnerEnum owner;

    protected CardAction[] selectedActions;
    protected CardAction[] deSelectedActions;
    protected CardAction[] playActions;
    protected CardAction[] drawActions;
    protected CardAction[] leaveActions;


    private void Start()
    {

    }

    public virtual void Setup(CardStatsBase cardStats, OwnerEnum ownerEnum )
    {
        cardName = cardStats.DisplayName;
        cardValue = cardStats.Value;
        cardSuite = cardStats.suite;
        owner = ownerEnum;
        selectedActions = cardStats.SelectedActions;
        deSelectedActions = cardStats.DeSelectedActions;
        playActions = cardStats.PlayActions;
        drawActions = cardStats.DrawActions;
        leaveActions = cardStats.LeaveActions;
    }

    public void TryDoSelectActions(GameObject target)
    {
        TryDoActions(selectedActions, target);
    }

    public void TryDoDeSelectActions(GameObject target)
    {
        TryDoActions(deSelectedActions, target);
    }

    public void TryDoPlayActions(GameObject target)
    {
        TryDoActions(playActions,target);
    }

    public void TryDoDrawActions(GameObject target)
    {
        TryDoActions(drawActions,target);
    }

    public void TryDoLeaveActions(GameObject target)
    {
        TryDoActions(leaveActions,target);
    }

    protected void TryDoActions(ICardAction[] actionsToDo, GameObject target)
    {
        foreach (var action in actionsToDo)
        {
            action.DoCardAction(this, target);
        }
    }
}
