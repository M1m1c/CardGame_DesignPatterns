using UnityEngine;

public interface IState
{
    public abstract void Enter(GameMaster owner, CardHolder target);
    public abstract void Exit(GameMaster owner, CardHolder target);
    public abstract StateLink[] GetStateLinks();
}


[CreateAssetMenu(fileName ="NewState",menuName ="State_Base")]
public class StateObjectBase : ScriptableObject ,IState
{
    [SerializeField] protected StateAction[] EntryActions;

    [SerializeField] protected StateAction[] ExitActions;

    [SerializeField] protected StateLink[] stateLinks;
  
    public virtual void Enter(GameMaster owner, CardHolder target)
    {
        foreach (var action in EntryActions)
        {
            action.DoAction(owner, target);
        }
    }

    public virtual void Exit(GameMaster owner, CardHolder target)
    {
        foreach (var action in ExitActions)
        {
            action.DoAction(owner, target);
        }
    }

    public StateLink[] GetStateLinks()
    {
        return stateLinks;
    }
}
