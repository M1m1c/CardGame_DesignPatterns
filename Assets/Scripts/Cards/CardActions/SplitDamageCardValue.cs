using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Action_SplitDamageCardValue", menuName = "Actions/Action_SplitDamageCardValue")]
public class SplitDamageCardValue : CardAction
{
    public override void DoAction(CardBase owner, GameObject target)
    {
        if (!owner) { return; }

        var targetCard = target.GetComponent<CardPlayable>();
        if (!targetCard) { return; }

        if (targetCard.Owner == owner.Owner) { return; }

        var parent = owner.transform.parent;
        if (!parent) { return; }

        if (parent == targetCard.transform.parent) { return; }

        var cardHolder = targetCard.transform.parent.GetComponent<CardHolder>();
        if (!cardHolder) { return; }

        var adjacentCardBase = cardHolder.GetAdjacentCards(targetCard);
        var adjacenPlayable = new List<CardPlayable>();
        foreach (var item in adjacentCardBase)
        {
            if (!item) { continue; }
            adjacenPlayable.Add(item.GetComponent<CardPlayable>());
        }

        var divideNumber = 3;

        var reductionValue = owner.CardValue/divideNumber;

        var unitCard = owner.GetComponent<UnitCardPlayable>();
        if (unitCard) { reductionValue = unitCard.DamageValue / divideNumber; }

        targetCard.ChangeCardValue(-reductionValue);

        foreach (var item in adjacenPlayable)
        {
            item.ChangeCardValue(-reductionValue);
        }

        if (owner.Type != CardType.Action) { return; }

        var hand = parent.GetComponent<CardHand>();
        if (hand)
        {
            hand.DestroyCard(owner);
        }
    }
}