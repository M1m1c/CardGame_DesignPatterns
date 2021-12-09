using UnityEngine;


[CreateAssetMenu(fileName = "Action_AIAttackSequence", menuName = "StateActions/Action_AIAttackSequence")]
public class AIAttackSequence : StateAction
{
    public override void DoAction(GameMaster owner, CardHolder target)
    {
        if (!target) { return; }

        var aiArea = target.GetComponent<AIArea>();
        if (!aiArea) { return; }

        aiArea.StartAttackingPlayer();
    }
}
