using System;
using System.Linq;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    public bool IsActive { get; set; } = false;

    protected float cardXOffset = 8.5f;
    protected float cardXStartPos = -17f;
    protected float[] cardXPosStarts = new float[] { 0f, -4.45f, -8.5f, -12.75f, -17f, -21.25f, };

    protected int currentHeldCount = 0;
    protected CardBase[] heldCards = new CardBase[5];

    public virtual void AddCard(CardBase card)
    {
        if (!card) { return; }
        if (currentHeldCount >= heldCards.Length) { return; }
        IncreaseCurrentHeldCount();
        heldCards[Array.IndexOf(heldCards, null)] = card;
        ReorganizeHeldCardPositions();
    }

    public virtual void RemoveCard(CardBase card)
    {
        if (!card) { return; }
        if (!heldCards.Contains(card)) { return; }

        var index = Array.IndexOf(heldCards, card);
        heldCards[index] = null;
        Destroy(card.gameObject);
        DecreaseCurrentHeldCount();
        ReorganizeHeldCardPositions();
    }

   
    //Moves heldCards so that they line up correctly on screen,
    //also pushes cards to lower positioned indecies that are empty.
    protected virtual void ReorganizeHeldCardPositions()
    {
        var pos = cardXStartPos;
        var xOffset = 0f;
        for (int i = 0; i < heldCards.Length; i++)
        {

            var card = heldCards[i];
            if (!card)
            {
                card = MoveNearestCardToIndex(i);
                if (!card) { break; }                
            }

            if (card.transform.parent != transform)
            {
                card.transform.parent = transform;
            }

            card.transform.position = this.transform.position + new Vector3(pos + xOffset, 0f, 0f);
            card.SlotedPosition = heldCards[i].transform.position;
            xOffset += cardXOffset;
        }
    }

    //moves the first found card to the i index in heldcards
    private CardBase MoveNearestCardToIndex(int i)
    {
        CardBase retval = null;
        for (int q = i; q < heldCards.Length; q++)
        {
            var cardToMove = heldCards[q];
            if (cardToMove)
            {
                //card = cardToMove;
                retval = cardToMove;
                heldCards[i] = cardToMove;
                heldCards[q] = null;
                break;
            }
        }
        return retval;
    }

    protected void IncreaseCurrentHeldCount()
    {
        cardXStartPos = cardXPosStarts[currentHeldCount];
        currentHeldCount++;
    }
    protected void DecreaseCurrentHeldCount()
    {
        currentHeldCount--;
        cardXStartPos = cardXPosStarts[Mathf.Clamp(currentHeldCount - 1, 0, cardXPosStarts.Length - 1)];
    }


    protected void SetCardPosAndSize(CardBase card, Vector3 posToSet, Vector3 sizeToSet)
    {
        card.transform.position = posToSet;
        card.transform.localScale = sizeToSet;
    }

    public bool DoesHeldCardsContainType(CardType cardType)
    {
        return heldCards.FirstOrDefault(q => q != null && q.Type == cardType);
    }

    public bool DoesHeldCardsContainType(CardType cardType, CardSuite cardSuite)
    {
        return heldCards.FirstOrDefault(q => q != null && q.Type == cardType && q.Suite==cardSuite);
    }

    public CardBase GetCard(int index)
    {
        return heldCards[index];
    }

    public int GetLength()
    {
        return heldCards.Length;
    }

    public int GetCurrentHeldCards()
    {
        return currentHeldCount;
    }
}
