using UnityEngine;

public interface IAction<TOwner,TTarget>
{
    public abstract void DoAction(TOwner owner, TTarget target);
}
