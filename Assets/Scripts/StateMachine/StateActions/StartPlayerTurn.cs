using UnityEngine;


[CreateAssetMenu(fileName = "Action_StartPlayerTurn", menuName = "StateActions/Action_StartPlayerTurn")]
public class StartPlayerTurn : StateAction
{
    public override void DoAction(GameMaster owner, CardHolder target)
    {
        if (!target) { return; }

        if(!owner) { return; }

        var cardHand = target.GetComponent<CardHand>();
        if (!cardHand) { return; }

        owner.SetupPlayerTurn();
        cardHand.DrawCardsPhase();

    }
}
