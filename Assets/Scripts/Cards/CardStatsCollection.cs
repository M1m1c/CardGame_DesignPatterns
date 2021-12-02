using UnityEngine;

[CreateAssetMenu(fileName = "NewCardStatCollection", menuName = "CardStatsCollection")]
public class CardStatsCollection : ScriptableObject
{
    public CardStatsBase[] CardStats;
}
