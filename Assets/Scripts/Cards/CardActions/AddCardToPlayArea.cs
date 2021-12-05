using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Action_AddCardToPlayArea", menuName = "Actions/Action_AddCardToPlayArea")]
public class AddCardToPlayArea : CardAction
{
    public override void DoCardAction(CardBase owner, GameObject target)
    {
        if (!owner) { return; }

        var playArea = target.GetComponent<PlayArea>();
        if (!playArea) { return; }

        var parent = owner.transform.parent;
        if (!parent) { return; }

        var hand = parent.GetComponent<CardHand>();
        if (!hand) { return; }

        hand.RemoveCard(owner);
        playArea.AddCard(owner);
    }
}
