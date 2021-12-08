using UnityEngine;

[CreateAssetMenu(fileName = "Action_AddCardToPlayArea", menuName = "Actions/Action_AddCardToPlayArea")]
public class AddCardToPlayArea : CardAction
{
    public override void DoAction(CardBase owner, GameObject target)
    {
        if (!owner) { return; }

        var cardArea = target.GetComponent<CardArea>();
        if (!cardArea) { return; }

        if (cardArea.Owner != owner.Owner) { return; }

        var parent = owner.transform.parent;
        if (!parent) { return; }

        var hand = parent.GetComponent<CardHand>();
        if (!hand) { return; }


        hand.RemoveCard(owner);
        cardArea.AddCard(owner);
        hand.OnCardPlayed.Notify(owner);
    }
}
