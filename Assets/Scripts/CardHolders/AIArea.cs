using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIArea : CardArea
{
    public override void AddCard(CardBase card)
    {
        if (!card) { return; }
        if (currentHeldCount >= heldCards.Length) { return; }
        cardXStartPos = cardXPosStarts[currentHeldCount];
        heldCards[currentHeldCount] = card;
        currentHeldCount++;
        ReorganizeHeldCardPositions();
    }
    public override void RemoveCard()
    {
        //base.RemoveCard();
    }
}
