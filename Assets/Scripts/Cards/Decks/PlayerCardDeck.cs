using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PlayerCardDeck : CardDeck
{
    private int heroCardThrehsold = 12;
    private int maxCardRand = 53;

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
        }
        else
        {
            cardType = CardType.Action;
            //TODO remove suites not avilable from temp suites
        }

        //cardType = CardType.Hero;

        var chosenSuite = tempSuites[Random.Range(0, tempSuites.Count)];
        var chosenCardStat = AvilableCards.CardStats.FirstOrDefault(q => q.suite == chosenSuite && q.cardType == cardType);

        if (chosenCardStat.cardPrefab)
        {
            var cardInstance = Instantiate(chosenCardStat.cardPrefab, transform.position, transform.rotation);
            cardInstance.Setup(chosenCardStat);
            retVal = cardInstance;
        }

        return retVal;
    }
}
