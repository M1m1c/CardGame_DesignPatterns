using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewCardStats", menuName = "CardStatsBase")]
public class CardStatsBase : ScriptableObject
{
    public string Name;
    public int Value;
    public CardSuite suite;
    public Sprite Image;
}

[System.Serializable]
public enum CardSuite
{
    None,
    Clubs = 1 << 0,
    Spades = 1 << 1,
    Hearts = 1 << 2,
    Diamonds= 1 << 3
}
