using UnityEngine;


[CreateAssetMenu(fileName = "Condition_IsPlayerOutOfMoves", menuName = "Conditions/Condition_IsPlayerOutOfMoves")]
public class IsPlayerOutOfMoves : LinkConditionBase
{
    public override bool CheckCondition(GameMaster owner, CardHolder target)
    {
        var retval = false;
        if (owner && target)
        {
            var cantPlayHeroCard = owner.IsHeroPlayedThisTurn() || !target.DoesHeldCardsContainType(CardType.Hero);
            if (owner.GetActionCardAllowence().Count == 0 && cantPlayHeroCard)
            {
                //Player turn is over, transition to ai turn
                Debug.Log("Player turn over");
                retval = true;
            }
        }

        return retval;
    }

}
