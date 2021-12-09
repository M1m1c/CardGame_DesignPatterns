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

    public Subject<CardHolder> OnTurnEnd { get; private set; } = new Subject<CardHolder>();

    public CardArea()
    {
        heldCards = new CardBase[6];
    }

    public virtual void AddCard(CardBase card)
    {
        if (!card) { return; }
        if (currentHeldCount >= heldCards.Length) { return; }
        cardXStartPos = cardXPosStarts[currentHeldCount];
        heldCards[Array.IndexOf(heldCards, null)] = card;
        currentHeldCount++;
        ReorganizeHeldCardPositions();
    }

    public virtual void RemoveCard(CardBase card)
    {
        if (!card) { return; }
        if (!heldCards.Contains(card)) { return; }

        var index = Array.IndexOf(heldCards, card);
        heldCards[index] = null;
        Destroy(card.gameObject);
        currentHeldCount--;
        cardXStartPos = cardXPosStarts[Mathf.Clamp(currentHeldCount - 1, 0, cardXPosStarts.Length - 1)];
        ReorganizeHeldCardPositions();

        if (currentHeldCount <= 0)
        {
            OnTurnEnd.Notify(this);
        }
    }

}
