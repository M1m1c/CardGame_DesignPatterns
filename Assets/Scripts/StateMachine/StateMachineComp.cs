using UnityEngine;

public class StateMachineComp : MonoBehaviour
{

    public StateObjectBase CurrentState;
    protected CardHolder currentHolder;

    protected StateInitialiser stateInitialiserComp;

    private void Awake()
    {
        stateInitialiserComp = GetComponent<StateInitialiser>();
    }

    //gets a reference to a new state if it is viable, then swithces to it
    public void CheckShouldStateTransition(CardHolder sender)
    {

        if (!CurrentState) { return; }

        if (!sender) { sender = currentHolder; }

        StateObjectBase PotentialNewState = CheckStateLinks(sender);

        if (PotentialNewState == null) { return; }
        if (PotentialNewState == CurrentState) { return; }

        SwitchState(PotentialNewState);
    }

    //Checks the link condition of each of this states links using the sender as the target of those checks
    //returns a the state a link leads to if the conditions are all true
    private StateObjectBase CheckStateLinks(CardHolder sender)
    {
        StateObjectBase PotentialNewState = null;
        foreach (var link in CurrentState.GetStateLinks())
        {
            if (link.LinkConditions.Length == 0)
            {
                PotentialNewState = link.LinkedState;
                break;
            }

            var correctConditions = 0;
            foreach (var condition in link.LinkConditions)
            {
                if (condition.CheckCondition(GameMaster.Instance, sender)) { correctConditions++; }
            }

            if (correctConditions == link.LinkConditions.Length)
            {
                PotentialNewState = link.LinkedState;
                break;
            }
        }

        return PotentialNewState;
    }

    //exits the current state and enters a new state
    private void SwitchState(StateObjectBase PotentialNewState)
    {
        CurrentState.Exit(GameMaster.Instance, currentHolder);
        CurrentState = PotentialNewState;
        currentHolder = stateInitialiserComp.IdHoldersPair[CurrentState.HolderId];
        CurrentState.Enter(GameMaster.Instance, currentHolder);
    }

}
