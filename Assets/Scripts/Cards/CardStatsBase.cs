using UnityEngine;


[CreateAssetMenu(fileName = "NewCardStats", menuName = "Cards/CardStatsBase")]
public class CardStatsBase : ScriptableObject
{
    public string DisplayName;
    public int Value;
    public CardSuite suite;
    public CardType cardType;
    public Texture2D Image;

    public CardBase cardPrefab;

    public CardAction[] SelectedActions;
    public CardAction[] DeSelectedActions;
    public CardAction[] PlayActions;
    public CardAction[] DrawActions;
    public CardAction[] LeaveActions;
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

[System.Serializable]
public enum OwnerEnum
{
    none,
    Player = 1 << 0,
    AI = 1 << 1
}
