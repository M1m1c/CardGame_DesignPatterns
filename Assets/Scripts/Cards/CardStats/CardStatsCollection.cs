using UnityEngine;

[CreateAssetMenu(fileName = "NewCardStatCollection", menuName = "Cards/CardStatsCollection")]
public class CardStatsCollection : ScriptableObject
{
    public CardStatsBase[] CardStats;
}
