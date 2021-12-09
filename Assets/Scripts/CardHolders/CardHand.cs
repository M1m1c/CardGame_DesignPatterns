using System;
using System.Linq;
using UnityEngine;

public class CardHand : CardHolder
{
    public Subject<CardBase> OnCardPlayed { get; private set; } = new Subject<CardBase>();

    public CardDeck cardDeck;

    private CardBase highLightedCard = null;
    private CardBase selectedCard = null;
    private LineRenderer targetingLine = null;

    private Ray ray;
    private RaycastHit hit;

    //Removes card form hand without destroying it.
    public override void RemoveCard(CardBase card)
    {
        if (!card) { return; }
        var isInHand = heldCards.Contains(card);
        if (!isInHand) { return; }

        DeSelectCardInHand();
        heldCards[Array.IndexOf(heldCards, card)] = null;
        DecreaseCurrentHeldCount();
        ReorganizeHeldCardPositions();
    }

    //Draws cards until hand is full
    public void DrawCardsPhase()
    {
        DestroyUnavailableActionCards();

        while (currentHeldCount < heldCards.Length)
        {
            var card = cardDeck.CreateCard();
            if (card)
            {
                AddCard(card);
            }
        }
        ReorganizeHeldCardPositions();
    }

    //Removes cards that we no longer get allowence for,
    //only happens if the last hero of a suite dies.
    private void DestroyUnavailableActionCards()
    {
        foreach (var heldCard in heldCards)
        {
            if (!heldCard) { continue; }
            var unavilable = GameMaster.Instance.GetUnavailableSuites();
            foreach (var suite in unavilable)
            {
                if (heldCard.Type == CardType.Action && heldCard.Suite == suite)
                {
                    DestroyCard(heldCard);
                }
            }
        }
    }

    //RemovesCard and destroys it
    public void DestroyCard(CardBase card)
    {
        if (!card) { return; }
        var isInHand = heldCards.Contains(card);
        if (!isInHand) { return; }

        RemoveCard(card);
        OnCardPlayed.Notify(card);
        Destroy(card.gameObject);
    }

    //Checks for input.
    //Tracks which held card is beignhovered over wiht mouse.
    //When clicking depending on context either plays card, selects card or de-selects card.
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) { Application.Quit(); }

        if (IsActive == false) { return; }
        RayCastFromMousePosition();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            AttemptPlayCard();
            SelectCardInHand();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            DeSelectCardInHand();
        }
    }

    private void RayCastFromMousePosition()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            HoverOverCardInHand();
            UpdateTargetingLine();
        }
    }

    //Highlights held card overlaped by the mouse cursor
    private void HoverOverCardInHand()
    {
        if (selectedCard) { return; }

        var entity = hit.collider.gameObject;
        if (!entity) { return; }

        //Removes highlight from current card when mouse it not over it
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

        //highlights card which mouse is over
        if (heldCards.Contains(card))
        {
            SetHighLightedCard(
                card,
                card.SlotedPosition + new Vector3(0f, 0f, 2f),
                card.OriginalSize + new Vector3(0.2f, 0.2f, 0.2f));
        }
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


    //Updates targetign line of currently seleceted card to follow mouse cursor.
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

    //Attempts to play card with current overed object as target of card
    private void AttemptPlayCard()
    {
        if (!selectedCard) { return; }

        var gameMaster = GameMaster.Instance;

        if (selectedCard.Type == CardType.Hero)
        {
            if (gameMaster.IsHeroPlayedThisTurn() == true)
            { return; }
        }
        else if (selectedCard.Type == CardType.Action)
        {
            var allowence = gameMaster.GetActionCardAllowence();
            if (!allowence.Contains(selectedCard.Suite) ||
                allowence.Count == 0)
            { return; }
        }

        var target = hit.collider.gameObject;
        if (!target) { return; }

        selectedCard.TryDoPlayActions(target);
    }

    private void SelectCardInHand()
    {
        if (highLightedCard && selectedCard == null)
        {
            selectedCard = highLightedCard;
            selectedCard.TryDoSelectActions(null);
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
}
