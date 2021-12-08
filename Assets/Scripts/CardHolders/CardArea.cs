using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public virtual void RemoveCard(CardBase card)
    {
        if (!card) { return; }
        if (!heldCards.Contains(card)) { return; }

        var index=Array.IndexOf(heldCards, card);
        heldCards[index] = null;
        Destroy(card.gameObject);
        currentHeldCount--;
        cardXStartPos = cardXPosStarts[Mathf.Clamp(currentHeldCount - 1, 0, cardXPosStarts.Length - 1)];
        ReorganizeHeldCardPositions();
    }
  
}
