using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private LineRenderer targetingLine = null;

    private Ray ray;
    private RaycastHit hit;

    //TODO needs a list of cards,
    //a way of highlighting cards that the mouse pases over,
    //a way of selecting cards in hand,
    //a way of playing cards

    // Start is called before the first frame update
    void Start()
    {
        DrawCardsPhase();
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            HoverOverCardInHand();
            UpdateTargetingLine();
        }



        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SelectCardInHand();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            DeSelectCardInHand();
        }
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


   

    private void HoverOverCardInHand()
    {
        if (selectedCard) { return; }

        var entity = hit.collider.gameObject;
        if (!entity) { return; }

        if (highLightedCard && entity != highLightedCard.gameObject)
        {
            SetHighLightedCard(
               null,
               highLightedCard.SlotedPosition,
               highLightedCard.OriginalSize);
            return;
        }

        var card = entity.GetComponent<CardBase>();
        if (!card) { return; }


        if (heldCards.Contains(card))
        {
            SetHighLightedCard(
                card,
                card.SlotedPosition + new Vector3(0f, 0f, 2f),
                card.OriginalSize + new Vector3(0.2f, 0.2f, 0.2f));
        }
    }

  

    private void DeSelectCardInHand()
    {
        if (selectedCard)
        {
            SetCardPosAndSize(selectedCard, selectedCard.SlotedPosition, selectedCard.OriginalSize);
            selectedCard.TryDoDeSelectActions(null);
            selectedCard = null;
            highLightedCard = null;
        }
    }

    private void SelectCardInHand()
    {
        if (highLightedCard && selectedCard == null)
        {
            selectedCard = highLightedCard;
            selectedCard.TryDoSelectActions(null);
        }
    }

    private void UpdateTargetingLine()
    {
        if (!selectedCard) { return; }

        if (!targetingLine)
        {
            targetingLine = selectedCard.GetComponent<LineRenderer>();
            return;
        }

        targetingLine.SetPosition(1, hit.point);

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

    private void SetCardPosAndSize(CardBase card, Vector3 posToSet, Vector3 sizeToSet)
    {
        card.transform.position = posToSet;
        card.transform.localScale = sizeToSet;
    }
}
