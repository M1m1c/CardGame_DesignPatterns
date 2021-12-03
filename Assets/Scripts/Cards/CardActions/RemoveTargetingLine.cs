using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Action_RemoveTargetingLine", menuName = "Actions/Action_RemoveTargetingLine")]
public class RemoveTargetingLine : CardAction
{
    public override void DoCardAction(CardBase owner, GameObject target)
    {
        if (!owner) { return; }
        var lineRenderer = owner.GetComponent<LineRenderer>();
        if (!lineRenderer) { return; }
        Destroy(lineRenderer);
    }
}
