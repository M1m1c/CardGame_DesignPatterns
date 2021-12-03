using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Action_AddTargetingLine", menuName = "Actions/Action_AddTargetingLine")]
public class AddTargetingLine : CardAction
{
    public override void DoCardAction(CardBase owner, GameObject target)
    {
        if (!owner) { return; }
        if (owner.GetComponent<LineRenderer>()) { return; }
        var lineRenderer=owner.gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, owner.transform.position);
        lineRenderer.SetPosition(1, owner.transform.position);
    }
}
