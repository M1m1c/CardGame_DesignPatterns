using UnityEngine;


[CreateAssetMenu(fileName = "Condition_IsPlayerOutOfMoves", menuName = "Conditions/Condition_IsPlayerOutOfMoves")]
public class IsPlayerOutOfMoves : LinkConditionBase
{
    public override bool CheckCondition(GameMaster owner, CardHolder target)
    {
        var retval = false;
        if (owner && target && target is CardHand)
        {
            var cantPlayHeroCard = owner.IsHeroPlayedThisTurn() || !target.DoesHeldCardsContainType(CardType.Hero);

            var actions = owner.GetActionCardAllowence();
            var cantPlayActionCard = actions.Count == 0;
            if (!cantPlayActionCard)
            {
                foreach (var suite in owner.GetActionCardAllowence())
                {
                    cantPlayActionCard = !target.DoesHeldCardsContainType(CardType.Action, suite);
                    if (cantPlayActionCard == false) { break; }
                }
            }

            if (cantPlayHeroCard && cantPlayActionCard)
            {
                retval = true;
            }
        }

        return retval;
    }
}
