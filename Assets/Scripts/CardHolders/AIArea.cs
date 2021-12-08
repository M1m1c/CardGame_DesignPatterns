using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIArea : CardArea
{
    public Subject<CardHolder> OnUnitDeath { get; private set; } = new Subject<CardHolder>();

    public int cardsToDrawPerRound { get; set; } = 3;

    public CardDeck cardDeck;

    private void Start()
    {
        AddUnitsToArea();
    }

    private void AddUnitsToArea()
    {
        while (currentHeldCount < cardsToDrawPerRound)
        {
            var card = cardDeck.CreateCard();
            AddCard(card);
        }
    }

    public override void AddCard(CardBase card)
    {
        if (!card) { return; }
        if (currentHeldCount >= heldCards.Length) { return; }
        cardXStartPos = cardXPosStarts[currentHeldCount];
        heldCards[Array.IndexOf(heldCards, null)] = card;
        currentHeldCount++;
        ReorganizeHeldCardPositions();
    }
    public override void RemoveCard(CardBase card)
    {
        base.RemoveCard(card);
        OnTurnEnd.Notify(null);
    }

    private void AddUnitsToArea()
    {
        while (currentHeldCount < cardsToDrawPerRound)
        {
            var card = cardDeck.CreateCard();
            AddCard(card);
        }
    }
}
