using UnityEngine;

public interface IState
{
    public abstract void Setup();
    public abstract void HandleInput<TEntity, TInput>(TEntity entity, TInput input);
    public abstract void UpdateState<TEntity>(TEntity entity);
    public abstract StateLink[] GetStateLinks();
}


[CreateAssetMenu(fileName ="NewState",menuName ="State_Base")]
public class StateObjectBase : ScriptableObject ,IState
{
    [SerializeField] protected StateLink[] stateLinks;
  
    public virtual void Setup()
    {
        throw new System.NotImplementedException();
    }   

    public virtual void HandleInput<TEntity, TInput>(TEntity entity, TInput input)
    {
        throw new System.NotImplementedException();
    }

    public virtual void UpdateState<TEntity>(TEntity entity)
    {
        throw new System.NotImplementedException();
    }

    public StateLink[] GetStateLinks()
    {
        return stateLinks;
    }
}
