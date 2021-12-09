using UnityEngine;

[CreateAssetMenu(fileName = "Action_HealCardValue", menuName = "Actions/Action_HealCardValue")]
public class HealCardValue : CardAction
{
    public override void DoAction(CardBase owner, GameObject target)
    {
        if (!owner) { return; }

        var targetCard = target.GetComponent<CardPlayable>();
        if (!targetCard) { return; }

        if (targetCard.Owner != owner.Owner) { return; }

        var parent = owner.transform.parent;
        if (!parent) { return; }

        if (parent == targetCard.transform.parent) { return; }

        targetCard.ChangeCardValue(+owner.CardValue);

        if (owner.Type != CardType.Action) { return; }

        var hand = parent.GetComponent<CardHand>();
        if (hand)
        {
            hand.DestroyCard(owner);
        }
    }
}