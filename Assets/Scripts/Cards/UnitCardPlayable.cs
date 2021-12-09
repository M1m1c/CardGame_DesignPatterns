using UnityEngine;

public class UnitCardPlayable : CardPlayable
{
    public TextMesh DamageText;
    public int DamageValue { get; private set; }

    public override void Setup(CardStatsBase cardStats, OwnerEnum ownerEnum)
    {
        base.Setup(cardStats, ownerEnum);

        DamageValue = cardStats.Value / 2;
        DamageText.text = "" + DamageValue;
    }
}
