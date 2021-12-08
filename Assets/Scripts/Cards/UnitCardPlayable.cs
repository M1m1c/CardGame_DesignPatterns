using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCardPlayable : CardPlayable
{
    public TextMesh DamageText;
    protected int damageValue = 0;

    public override void Setup(CardStatsBase cardStats, OwnerEnum ownerEnum)
    {
        base.Setup(cardStats, ownerEnum);

        damageValue = cardStats.Value / 2;
        DamageText.text = "" + damageValue;
    }
}
