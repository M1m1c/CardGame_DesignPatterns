using UnityEngine;

[CreateAssetMenu(fileName = "Action_RemoveTargetingLine", menuName = "Actions/Action_RemoveTargetingLine")]
public class RemoveTargetingLine : CardAction
{
    public override void DoAction(CardBase owner, GameObject target)
    {
        if (!owner) { return; }
        var lineRenderer = owner.GetComponent<LineRenderer>();
        if (!lineRenderer) { return; }
        Destroy(lineRenderer);
    }
}
