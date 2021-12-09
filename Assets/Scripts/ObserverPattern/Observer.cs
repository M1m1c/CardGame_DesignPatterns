using System;

public class Observer<TEntity, TEventData>
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


public class Observer<TEventData>
{
    public Observer<TEventData> Next;
    protected Action<TEventData> ActionToInvoke;


    public Observer(Action<TEventData> action)
    {
        ActionToInvoke = action;
    }


    public virtual void OnNotify(TEventData eventData)
    {
        ActionToInvoke.Invoke(eventData);
        if (Next != null) { Next.OnNotify(eventData); }
    }
}
