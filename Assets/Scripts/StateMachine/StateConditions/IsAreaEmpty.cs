using UnityEngine;


[CreateAssetMenu(fileName = "Condition_IsAreaEmpty", menuName = "Conditions/Condition_IsAreaEmpty")]
public class IsAreaEmpty : LinkConditionBase
{
    public override bool CheckCondition(GameMaster owner, CardHolder target)
    {
        var retval = false;
        if (owner && target)
        {
            if((target is CardArea)) 
            {
                if (target.GetCurrentHeldCards() <= 0) { retval = true; }
            }                         
        }

        return retval;
    }

}
