using UnityEngine;

[CreateAssetMenu(fileName = "NewActionCardStats", menuName = "Cards/ActionCardStats")]
public class ActionCardStats : CardStatsBase
{
    //This class is specifically used for action cards that need to randomise their card value.
    public ActionCardStats(ActionCardStats statsToUse)
    {
        DisplayName = statsToUse.DisplayName;
        Value = Random.Range(statsToUse.MinValue, statsToUse.MaxValue);
        suite = statsToUse.suite;
        cardType = statsToUse.cardType;
        Image = statsToUse.Image;
        SuiteIcon = statsToUse.SuiteIcon;

        cardPrefab = statsToUse.cardPrefab;

        SelectedActions = statsToUse.SelectedActions;
        DeSelectedActions = statsToUse.DeSelectedActions;
        PlayActions = statsToUse.PlayActions;
        LeaveActions = statsToUse.LeaveActions;
    }
    public int MinValue = 0;
    public int MaxValue = 0;
}
