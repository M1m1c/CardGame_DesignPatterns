
using UnityEngine;

public interface ICardAction
{
    public abstract void DoCardAction(CardBase owner, GameObject target);
}

public class CardAction : ScriptableObject, ICardAction
{
    public virtual void DoCardAction(CardBase owner, GameObject target)
    {
        throw new System.NotImplementedException();
    }
}