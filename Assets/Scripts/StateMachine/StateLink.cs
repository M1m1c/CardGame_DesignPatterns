using UnityEngine;

[System.Serializable]
public struct StateLink 
{
    [SerializeField] private StateObjectBase linkedState;

    public StateObjectBase LinkedState
    {
        get { return linkedState; }
    }

    [SerializeField] private LinkConditionBase[] linkConditions;

    public LinkConditionBase[] LinkConditions
    {
        get { return linkConditions; }
    }

}
