using UnityEngine;

public class CardBase : MonoBehaviour
{
    protected string cardName;

    protected CardAction[] playActions;
    protected CardAction[] drawActions;
    protected CardAction[] leaveActions;


    public void Play()
    {
        //TODO probably needs a destination slot to place it at
    }

    public void Select()
    {

    }

    public void Remove()
    {

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

    void Start()
    {
        
    }

  
}
