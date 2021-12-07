using UnityEngine;

public interface ILinkCondition<TOwner,TTarget>
{
    public abstract bool CheckCondition(TOwner owner,TTarget target);    
}

public class LinkConditionBase : ScriptableObject,  ILinkCondition<GameMaster,CardHolder>
{
    public virtual bool CheckCondition(GameMaster owner,CardHolder target)
    {
        throw new System.NotImplementedException();
    }
}
