using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICardDeck : CardDeck
{
    [SerializeField]
    protected int deckSize = 40; 
    public override CardBase CreateCard()
    {
        if (AvilableCards.CardStats.Length <= 0) { return null; }
        if (deckSize < 0) 
        { 
            //TODO implement game end call
            return null; 
        }
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
                deckSize--;
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
