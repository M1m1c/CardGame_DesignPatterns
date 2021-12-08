using UnityEngine;


[CreateAssetMenu(fileName = "Condition_IsOutOfEnemies", menuName = "Conditions/Condition_IsOutOfEnemies")]
public class IsOutOfEnemies : LinkConditionBase
{
    public override bool CheckCondition(GameMaster owner, CardHolder target)
    {
        var retval = false;
        if (owner && target)
        {
            if(!(target is CardHand)) 
            {
                if (target.GetCurrentHeldCards() <= 0) { retval = true; }
            }                         
        }

        return retval;
    }

}
