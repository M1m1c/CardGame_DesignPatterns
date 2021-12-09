using UnityEngine;

[CreateAssetMenu(fileName = "Action_IncreaseHeroCardChance", menuName = "Actions/Action_IncreaseHeroCardChance")]
public class IncreaseHeroCardChance : CardAction
{
    public override void DoAction(CardBase owner, GameObject target)
    {
        if (!owner) { return; }

        var playerDeck = FindObjectOfType<PlayerCardDeck>();
        if (!playerDeck) { return; }
        playerDeck.ChangeHeroCardThreshold(+5);
    }
}
