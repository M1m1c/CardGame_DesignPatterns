using UnityEngine;

public interface ILinkCondition
{
    public abstract bool CheckCondition<TEntity>(TEntity entity);    
}

public class LinkConditionBase : MonoBehaviour,  ILinkCondition
{
    public virtual bool CheckCondition<TEntity>(TEntity entity)
    {
        throw new System.NotImplementedException();
    }
}
