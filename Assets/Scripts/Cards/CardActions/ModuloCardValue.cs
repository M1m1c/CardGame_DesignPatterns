using UnityEngine;

[CreateAssetMenu(fileName = "Action_ModuloCardValue", menuName = "Actions/Action_ModuloCardValue")]
public class ModuloCardValue : CardAction
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

        var reductionValue = (targetCard.CardValue / owner.CardValue) * owner.CardValue;

        var unitCard = owner.GetComponent<UnitCardPlayable>();
        if (unitCard) { reductionValue = (targetCard.CardValue / unitCard.DamageValue) * unitCard.DamageValue; }

        if (targetCard.CardValue == 1 || reductionValue == 0) { reductionValue = 1; }

        targetCard.ChangeCardValue(-reductionValue);

        if (owner.Type != CardType.Action) { return; }

        var hand = parent.GetComponent<CardHand>();
        if (hand)
        {
            hand.DestroyCard(owner);
        }
    }
}