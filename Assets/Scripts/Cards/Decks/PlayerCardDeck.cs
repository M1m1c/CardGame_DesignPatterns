using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PlayerCardDeck : CardDeck
{
    private int heroCardThrehsold = 25;
    private int maxCardRand = 101;

    public void ChangeHeroCardThreshold(int chagenValue)
    {
        heroCardThrehsold += chagenValue;
    }

    public override CardBase CreateCard()
    {
        if (AvilableCards.CardStats.Length <= 0) { return null; }
        CardBase retVal = null;

        var tempSuites = suites.ToList();
        CardType cardType = CardType.None;

        var isHeroCard = Random.Range(0, maxCardRand) < heroCardThrehsold;
        if (isHeroCard)
        {
            cardType = CardType.Hero;
            ChangeHeroCardThreshold(-5);
        }
        else
        {
            cardType = CardType.Action;
            var unavailableSuites = GameMaster.Instance.GetUnavailableSuites();
            foreach (var uSuite in unavailableSuites)
            {
                tempSuites.Remove(uSuite);
            }
        }

        //cardType = CardType.Hero;

        var chosenSuite = tempSuites[Random.Range(0, tempSuites.Count)];
        var chosenCardStat = AvilableCards.CardStats.FirstOrDefault(q => q.cardType == cardType && q.suite == chosenSuite);

        if (chosenCardStat != null && chosenCardStat.suite != CardSuite.None)
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
        cardInstance.Setup(chosenCardStat, OwnerEnum.Player);
        return cardInstance;
    }
}
