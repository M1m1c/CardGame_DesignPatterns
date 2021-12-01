using UnityEngine;

public class CardBase : MonoBehaviour
{
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
        cardName = cardStats.name;
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

}
