using UnityEngine;

public abstract class CardDeck : MonoBehaviour
{
    public CardStatsCollection AvilableCards;
    protected CardSuite[] suites = new CardSuite[] { CardSuite.Clubs, CardSuite.Spades, CardSuite.Hearts, CardSuite.Diamonds };

    public abstract CardBase CreateCard();

    public abstract CardBase CreateCardBasedOnStats(CardStatsBase chosenCardStat);

}
