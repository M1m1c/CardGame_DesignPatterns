using UnityEngine;

public class SubjectComp : MonoBehaviour
{
    public int observerCount { get; private set; }
    private ObserverComp head;

    public void AddObserver(ObserverComp observerToAdd)
    {
        observerToAdd.Next = head;
        head = observerToAdd;
    }

    public void RemoveObserver(ObserverComp observerToRemove)
    {
        if (head == observerToRemove)
        {
            head = observerToRemove.Next;
            observerToRemove.Next = null;
            return;
        }

        var current = head;

        while (current != null)
        {
            if (current.Next == observerToRemove)
            {
                current.Next = observerToRemove.Next;
                observerToRemove.Next = null;
                return;
            }
            current = current.Next;
        }
    }

    protected void Notify<TEntity, TEventData>(TEntity entity, TEventData eventData)
    {
        if (head == null) { return; }
        head.OnNotify<TEntity, TEventData>(entity, eventData);
    }
}
