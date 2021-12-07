using UnityEngine;

public class StateAction : ScriptableObject, IAction<GameMaster,CardHolder>
{
    public virtual void DoAction(GameMaster owner, CardHolder target)
    {
        throw new System.NotImplementedException();
    }
}
