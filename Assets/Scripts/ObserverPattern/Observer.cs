using System;
using UnityEngine;

public class Observer<TEntity,TEventData>
{
    public Observer<TEntity, TEventData> Next;
    protected Action<TEntity, TEventData> ActionToInvoke;


    public Observer(Action<TEntity, TEventData> action)
    {
        ActionToInvoke = action;
    }


    public virtual void OnNotify(TEntity entity, TEventData eventData)
    {
        ActionToInvoke.Invoke(entity, eventData);
        if (Next != null) { Next.OnNotify(entity, eventData); }
    }
}
