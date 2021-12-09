using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AIArea : CardArea
{
    public int cardsToDrawPerRound { get; private set; } = 1;
    public Text SlideNumber;

    public void SetcardsToDrawPerRound(System.Single value)
    {
        cardsToDrawPerRound=(int)value;
        if (SlideNumber) { SlideNumber.text = "" + value; }
    }

    public CardDeck cardDeck;

    public PlayerArea playerArea;

    public void StartAttackingPlayer()
    {
        if (!playerArea) { return; }
        if (!IsActive) { return; }
        StartCoroutine(AttackSequence());
    }

    private IEnumerator AttackSequence()
    {
      
        for (int i = 0; i < heldCards.Length; i++)
        {
            var card = heldCards[i];
            if (!card) { continue; }
            yield return StartCoroutine(AttackWithCard(card));
        }
        StopAllCoroutines();
        OnTurnEnd.Notify(this);
    }

    private IEnumerator AttackWithCard(CardBase card)
    {
        if (!card) { yield break; }
        if (playerArea.GetCurrentHeldCards() <= 0) { yield break; }
        CardBase target = null;
        while (target == null)
        {
            target = playerArea.GetCard(UnityEngine.Random.Range(0, playerArea.GetLength()));
        }

     
        card.TryDoSelectActions(target.gameObject);

        var line = card.GetComponent<LineRenderer>();
        if (line) { line.SetPosition(1, target.transform.position); }

        card.TryDoPlayActions(target.gameObject);

        SetCardPosAndSize(
            card,
            card.SlotedPosition + new Vector3(0f, 0f, -2f),
            card.OriginalSize + new Vector3(0.2f, 0.2f, 0.2f));

        yield return new WaitForSeconds(1f);

        SetCardPosAndSize(card, card.SlotedPosition, card.OriginalSize);
        card.TryDoDeSelectActions(null);
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

    public void AddUnitsToArea()
    {
        while (currentHeldCount < cardsToDrawPerRound)
        {
            var card = cardDeck.CreateCard();
            AddCard(card);
        }
    }
}
