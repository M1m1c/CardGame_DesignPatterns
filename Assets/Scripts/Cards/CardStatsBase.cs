using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewCardStats", menuName = "CardStatsBase")]
public class CardStatsBase : ScriptableObject
{
    public string Name;
    public int Value;
    public CardSuite suite;
    public CardType cardType;
    public Texture2D Image;

    public CardBase cardPrefab;

    public CardAction[] PlayActions;
    public CardAction[] DrawActions;
    public CardAction[] LeaveActions;
}

[CreateAssetMenu(fileName = "NewCardStatCollection", menuName = "CardStatsCollection")]
public class CardStatsCollection : ScriptableObject
{
    public CardStatsBase[] CardStats;
}

[System.Serializable]
public enum CardSuite
{
    None,
    Clubs = 1 << 0,
    Spades = 1 << 1,
    Hearts = 1 << 2,
    Diamonds = 1 << 3
}

[System.Serializable]
public enum CardType
{
    None,
    Hero = 1 << 0,
    Action = 1 << 1,
    Unit = 1 << 2
}
