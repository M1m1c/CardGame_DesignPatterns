
public class Subject<TEntity,TEventData>
{
    public int observerCount { get; private set; }
    private Observer<TEntity, TEventData> head;

    public void AddObserver(Observer<TEntity, TEventData> observerToAdd)
    {
        observerToAdd.Next = head;
        head = observerToAdd;
    }

    public void RemoveObserver(Observer<TEntity, TEventData> observerToRemove)
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

    public void Notify(TEntity entity, TEventData eventData)
    {
        if (head == null) { return; }
        head.OnNotify(entity, eventData);
    }
}


public class Subject<TEventData>
{
    public int observerCount { get; private set; }
    private Observer<TEventData> head;

    public void AddObserver(Observer<TEventData> observerToAdd)
    {
        if (observerToAdd == null) { return; }
        observerToAdd.Next = head;
        head = observerToAdd;
    }

    public void RemoveObserver(Observer<TEventData> observerToRemove)
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

    public void Notify( TEventData eventData)
    {
        if (head == null) { return; }
        head.OnNotify( eventData);
    }
}
