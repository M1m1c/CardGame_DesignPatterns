using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardArea : CardHolder
{
    [SerializeField]
    private OwnerEnum owner;
    public OwnerEnum Owner { get { return owner; } }

    public CardArea()
    {
        heldCards = new CardBase[6];
    }

    public abstract void AddCard(CardBase card);

    public abstract void RemoveCard();
  
}
