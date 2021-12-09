using System.Collections.Generic;
using UnityEngine;

public class StateInitialiser : MonoBehaviour
{
    public Dictionary<int, CardHolder> IdHoldersPair { get; private set; } = new Dictionary<int, CardHolder>();

    [SerializeField]
    protected StateInstancePair[] stateInstancePairs;

    //Setsup the correct ID for the related CardHolder in the game,
    //this ID is later used by the GameMaster to determine what the current odler is when switching state.
    private void Awake()
    {
        foreach (var pair in stateInstancePairs)
        {
            if (!pair.HolderToId) { continue; }

            var id = pair.HolderToId.GetInstanceID();
            pair.State.HolderId = id;

            IdHoldersPair.Add(id, pair.HolderToId);
        }
        IdHoldersPair.Add(0, null);
    }
}

[System.Serializable]
public struct StateInstancePair
{
    public CardHolder HolderToId;
    public StateObjectBase State;
}
