using UnityEngine;


[CreateAssetMenu(fileName = "Action_RoundOver", menuName = "StateActions/Action_RoundOver")]
public class RoundOverAction : StateAction
{
    public override void DoAction(GameMaster owner, CardHolder target)
    {
        if(!owner) { return; }
        owner.RoundOver();
    }
}
