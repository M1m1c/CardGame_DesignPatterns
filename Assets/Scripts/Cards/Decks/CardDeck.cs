using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO make this into a factory pattern or somethign similar, that creates and stores a stack of cards
public abstract class CardDeck : MonoBehaviour
{
    public CardStatsCollection AvilableCards;
    protected CardSuite[] suites = new CardSuite[] { CardSuite.Clubs, CardSuite.Spades, CardSuite.Hearts, CardSuite.Diamonds };

    public abstract CardBase CreateCard();

    public abstract CardBase CreateCardBasedOnStats(CardStatsBase chosenCardStat);

}
