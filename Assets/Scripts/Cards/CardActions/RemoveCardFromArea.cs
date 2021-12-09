using UnityEngine;

[CreateAssetMenu(fileName = "Action_RemoveCardFromArea", menuName = "Actions/Action_RemoveCardFromArea")]
public class RemoveCardFromArea : CardAction
{
    public override void DoAction(CardBase owner, GameObject target)
    {
        if (!owner) { return; }

        var cardArea = target.GetComponent<CardArea>();
        if (!cardArea) { return; }

        if (cardArea.Owner != owner.Owner) { return; }

        var parent = owner.transform.parent;
        if (!parent) { return; }

        cardArea.RemoveCard(owner);
    }
}
