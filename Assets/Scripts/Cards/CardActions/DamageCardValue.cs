using UnityEngine;

[CreateAssetMenu(fileName = "Action_DamageCardValue", menuName = "Actions/Action_DamageCardValue")]
public class DamageCardValue : CardAction
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

        var reductionValue = owner.CardValue;

        var unitCard = owner.GetComponent<UnitCardPlayable>();
        if (unitCard) { reductionValue = unitCard.DamageValue; }

        targetCard.ChangeCardValue(-reductionValue);

        if (owner.Type != CardType.Action) { return; }

        var hand = parent.GetComponent<CardHand>();
        if (hand)
        {
            hand.DestroyCard(owner);
        }
    }
}