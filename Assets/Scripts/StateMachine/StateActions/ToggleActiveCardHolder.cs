using UnityEngine;

[CreateAssetMenu(fileName = "Action_ToggleActiveCardHolder", menuName = "StateActions/Action_ToggleActiveCardHolder")]
public class ToggleActiveCardHolder : StateAction
{
    public override void DoAction(GameMaster owner, CardHolder target)
    {
        if (!target) { return; }        
        target.IsActive = !target.IsActive;
    }
}
