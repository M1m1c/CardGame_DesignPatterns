using UnityEngine;


public class CardAction : ScriptableObject, IAction<CardBase,GameObject>
{
    public virtual void DoAction(CardBase owner, GameObject target)
    {
        throw new System.NotImplementedException();
    }
}