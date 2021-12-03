using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayArea : CardHolder
{

    void Start()
    {
        heldCards = new CardBase[6];
    }

    public void AddCard(CardBase card)
    {
        if (!card) { return; }
        if (currentHeldCount >= heldCards.Length) { return; }
        cardXStartPos = cardXPosStarts[currentHeldCount];
        heldCards[currentHeldCount] = card;
        currentHeldCount++;
        ReorganizeHeldCardPositions();
    }
    public void RemoveCard()
    {

    }

}
