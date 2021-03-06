using UnityEngine;

public class CardBase : MonoBehaviour
{
    public Vector3 SlotedPosition { get; set; }
    public Vector3 OriginalSize { get; private set; } = new Vector3(1f, 1f, 1f);
    public int CardValue { get { return cardValue; } }
    public CardSuite Suite { get { return cardSuite; } }
    public CardType Type { get { return cardType; } }
    public OwnerEnum Owner { get { return owner; } }

    protected string cardName;
    protected int cardValue;
    protected CardSuite cardSuite;
    protected CardType cardType;
    protected OwnerEnum owner;

    protected CardAction[] selectedActions;
    protected CardAction[] deSelectedActions;
    protected CardAction[] playActions;
    protected CardAction[] leaveActions;

    public virtual void Setup(CardStatsBase cardStats, OwnerEnum ownerEnum)
    {
        cardName = cardStats.DisplayName;
        cardValue = cardStats.Value;
        cardSuite = cardStats.suite;
        cardType = cardStats.cardType;
        owner = ownerEnum;
        selectedActions = cardStats.SelectedActions;
        deSelectedActions = cardStats.DeSelectedActions;
        playActions = cardStats.PlayActions;
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
        TryDoActions(playActions, target);
    }

    public void TryDoLeaveActions(GameObject target)
    {
        TryDoActions(leaveActions, target);
    }

    protected void TryDoActions(CardAction[] actionsToDo, GameObject target)
    {
        foreach (var action in actionsToDo)
        {
            action.DoAction(this, target);
        }
    }
}
