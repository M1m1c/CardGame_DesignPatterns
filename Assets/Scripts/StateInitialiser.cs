using System.Collections.Generic;
using UnityEngine;

public class StateInitialiser : MonoBehaviour
{
    public Dictionary<int, CardHolder> IdHoldersPair { get; private set; } = new Dictionary<int, CardHolder>();

    [SerializeField]
    protected StateInstancePair[] stateInstancePairs;
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
