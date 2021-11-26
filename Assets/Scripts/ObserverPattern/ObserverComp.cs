using UnityEngine;

public class ObserverComp : MonoBehaviour
{
    public ObserverComp Next;
    public virtual void OnNotify<TEntity, TEventData>(TEntity entity, TEventData eventData)
    {
        //TODO do stuff

        if (Next) { Next.OnNotify(entity, eventData); }
    }
}
