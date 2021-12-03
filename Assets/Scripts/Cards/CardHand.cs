using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHand : MonoBehaviour
{

    public CardDeck cardDeck;

    private int currentHandCount = 0;

    private float cardXOfffest = 8.5f;
    private float cardXStartPos = -17f;//-21.25f;

    private CardBase[] heldCards = new CardBase[5];
    private CardBase highLightedCard = null;
    private CardBase selectedCard = null;

    //TODO needs a list of cards,
    //a way of highlighting cards that the mouse pases over,
    //a way of selecting cards in hand,
    //a way of playing cards

    // Start is called before the first frame update
    void Start()
    {
        DrawCardsPhase();
    }

    private void DrawCardsPhase()
    {
        while (currentHandCount < heldCards.Length)
        {
            if (DrawCard(currentHandCount))
            {
                currentHandCount++;
            }
        }
        ReorganizeHeldCardPositions();
    }

    private void ReorganizeHeldCardPositions()
    {
        var pos = cardXStartPos;
        var xOffset = 0f;
        for (int i = 0; i < currentHandCount; i++)
        {
            var card = heldCards[i];
            if (card.transform.parent == transform) { continue; }
            card.transform.position = this.transform.position + new Vector3(pos + xOffset, 0f, 0f);
            card.transform.parent = transform;
            card.SlotedPosition = heldCards[i].transform.position;
            card.HighLightedEvent.AddListener(SetHighLightedCard);
            xOffset += cardXOfffest;
        }
    }

    private bool DrawCard(int drawIndex)
    {
        var retval = false;
        var card = cardDeck.CreateCard();
        if (card)
        {
            heldCards[drawIndex] = card;
            retval = true;
        }
        return retval;
    }

    private void SetHighLightedCard(CardBase card, Vector3 posToSet, Vector3 sizeToSet)
    {
        if (highLightedCard && card == null)
        {
            SetCardPosAndSize(highLightedCard, posToSet, sizeToSet);
        }
        else if (card)
        {
            SetCardPosAndSize(card, posToSet, sizeToSet);
        }

        highLightedCard = card;
    }

    public bool IsThereACardSelection()
    {
        return selectedCard != null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (highLightedCard && selectedCard == null) { selectedCard = highLightedCard; }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (selectedCard)
            {
                SetCardPosAndSize(selectedCard, selectedCard.SlotedPosition, selectedCard.OriginalSize);
                selectedCard = null;
                highLightedCard = null;
            }
        }
    }

    private void SetCardPosAndSize(CardBase card, Vector3 posToSet, Vector3 sizeToSet)
    {
        card.transform.position = posToSet;
        card.transform.localScale = sizeToSet;
    }
}
