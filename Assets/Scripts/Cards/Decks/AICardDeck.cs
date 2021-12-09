using UnityEngine;

public class AICardDeck : CardDeck
{
    public override CardBase CreateCard()
    {
        if (AvilableCards.CardStats.Length <= 0) { return null; }
        CardBase retVal = null;

        var chosenCardStat = AvilableCards.CardStats[Random.Range(0,AvilableCards.CardStats.Length)];

        if (chosenCardStat != null)
        {
            if (chosenCardStat.cardPrefab)
            {
                if (chosenCardStat is ActionCardStats)
                {
                    chosenCardStat = new ActionCardStats((ActionCardStats)chosenCardStat);
                }

                var cardInstance = CreateCardBasedOnStats(chosenCardStat);
                retVal = cardInstance;
            }
        }

        return retVal;
    }

    public override CardBase CreateCardBasedOnStats(CardStatsBase chosenCardStat)
    {
        var cardInstance = Instantiate(chosenCardStat.cardPrefab, transform.position, transform.rotation);
        cardInstance.Setup(chosenCardStat, OwnerEnum.AI);
        return cardInstance;
    }
}
