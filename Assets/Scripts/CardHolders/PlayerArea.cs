using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArea : CardArea
{
    public Subject<List<CardSuite>> OnUpdateAvailableSuites { get; private set; } = new Subject<List<CardSuite>>();

    public override void AddCard(CardBase card)
    {
        if (!card) { return; }
        if (currentHeldCount >= heldCards.Length) { return; }
        cardXStartPos = cardXPosStarts[currentHeldCount];
        heldCards[Array.IndexOf(heldCards, null)] = card;
        currentHeldCount++;
        ReorganizeHeldCardPositions();

        var cardSuitesOnField = new List<CardSuite>();
        foreach (var item in heldCards)
        {
            if (item == null) { continue; }
            if (item.Suite == CardSuite.None) { continue; }
            cardSuitesOnField.Add(item.Suite);
        }
        OnUpdateAvailableSuites.Notify(cardSuitesOnField);
    }
}
