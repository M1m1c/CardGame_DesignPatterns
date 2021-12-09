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

    //Randomises if it should create a hero or action card, randomisese suite and finds the stats of that card,
    //instantiates a new card if stats matching the type and suite were found.
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


        var chosenSuite = tempSuites[Random.Range(0, tempSuites.Count)];
        var chosenCardStat = AvilableCards.CardStats.
            FirstOrDefault(q => q != null && q.cardType == cardType && q.suite == chosenSuite);

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
