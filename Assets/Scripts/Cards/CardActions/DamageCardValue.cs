using UnityEngine;

[CreateAssetMenu(fileName = "Action_DamageCardValue", menuName = "Actions/Action_DamageCardValue")]
public class DamageCardValue : CardAction
{
    public override void DoCardAction(CardBase owner, GameObject target)
    {
        if (!owner) { return; }

        var targetCard = target.GetComponent<CardPlayable>();
        if (!targetCard) { return; }

        if (targetCard.Owner == owner.Owner) { return; }

        var parent = owner.transform.parent;
        if (!parent) { return; }

        var hand = parent.GetComponent<CardHand>();
        if (!hand) { return; }

        targetCard.ChangeCardValue(-owner.CardValue);
        hand.RemoveCard(owner);
        Destroy(owner.gameObject);
    }
}