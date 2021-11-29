
using UnityEngine;

public interface ICardAction
{
    public abstract void DoCardAction();
}

public class CardAction : MonoBehaviour, ICardAction
{
    public virtual void DoCardAction()
    {
        throw new System.NotImplementedException();
    }
}