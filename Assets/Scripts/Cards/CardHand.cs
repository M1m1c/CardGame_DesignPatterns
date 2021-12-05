using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardHand : CardHolder
{

    public CardDeck cardDeck;

    

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
            AttemptPlayCard();


        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            DeSelectCardInHand();
        }
    }

    public void RemoveCard(CardBase tempSelection)
    {
        DeSelectCardInHand();
        heldCards[Array.IndexOf(heldCards, tempSelection)] = null;
        currentHeldCount--;

        var startPosIndex = currentHeldCount;
        if (currentHeldCount > 0) { startPosIndex--; }
        cardXStartPos = cardXPosStarts[startPosIndex];

        ReorganizeHeldCardPositions();
    }

    public void DestroyCard(CardBase card)
    {
        if (!card) { return; }

        var isInHand = heldCards.FirstOrDefault(q => q == card);
        if (!isInHand) { return; }

        Destroy(card.gameObject);
    }

    private void AttemptPlayCard()
    {
        if (!selectedCard) { return; }

        var target = hit.collider.gameObject;
        if (!target) { return; }

        selectedCard.TryDoPlayActions(target);
    }

    private void DrawCardsPhase()
    {
        while (currentHeldCount < heldCards.Length)
        {
            if (DrawCard(currentHeldCount))
            {
                cardXStartPos = cardXPosStarts[currentHeldCount];
                currentHeldCount++;
            }
        }
        ReorganizeHeldCardPositions();
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
