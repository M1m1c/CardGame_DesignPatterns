using UnityEngine;

[CreateAssetMenu(fileName = "NewActionCardStats", menuName = "Cards/ActionCardStats")]
public class ActionCardStats : CardStatsBase
{
    public ActionCardStats(ActionCardStats statsToUse)
    {
        DisplayName = statsToUse.DisplayName;
        Value = Random.Range(statsToUse.MinValue, statsToUse.MaxValue);
        suite = statsToUse.suite;
        cardType = statsToUse.cardType;
        Image = statsToUse.Image;

        cardPrefab = statsToUse.cardPrefab;

        SelectedActions = statsToUse.SelectedActions;
        DeSelectedActions = statsToUse.DeSelectedActions;
        PlayActions = statsToUse.PlayActions;
        DrawActions = statsToUse.DrawActions;
        LeaveActions = statsToUse.LeaveActions;
    }
    public int MinValue = 0;
    public int MaxValue = 0;
}
