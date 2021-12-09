using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMaster : MonoBehaviour
{


    public Subject<CardHolder> OnCheckTurnOver { get; private set; } = new Subject<CardHolder>();

    public bool HeroPlayedThisTurn { get; private set; } = false;

    public List<CardSuite> UnavailableSuites { get; private set; } = new List<CardSuite>() {
        CardSuite.Spades,
        CardSuite.Diamonds,
        CardSuite.Hearts };

    public List<CardSuite> ActionCardAllowence { get; private set; } = new List<CardSuite>() {
         CardSuite.Clubs,
        CardSuite.Spades,
        CardSuite.Diamonds,
        CardSuite.Hearts };

    protected CardSuite[] cardSuitesInGame = new CardSuite[] {
        CardSuite.Clubs,
        CardSuite.Spades,
        CardSuite.Diamonds,
        CardSuite.Hearts };

    protected List<CardSuite> HeroesInAreaAllowence = new List<CardSuite>();

    public void SetupCardAllowence()
    {
        ActionCardAllowence = HeroesInAreaAllowence.ToList();
        foreach (var suite in UnavailableSuites)
        {
            ActionCardAllowence.Remove(suite);
        }
        HeroPlayedThisTurn = false;
    }

    public void UpdateCardAllowence(CardBase cardToRemove)
    {
        if (cardToRemove.Type == CardType.Hero)
        {
            HeroPlayedThisTurn = true;
        }
        else
        {
            var suite = cardToRemove.Suite;

            if (ActionCardAllowence.Contains(suite))
            {
                ActionCardAllowence.Remove(suite);
            }
        }

        OnCheckTurnOver.Notify(null);
    }

    public void UpdateUnavialableSuites(List<CardSuite> suitesInArea)
    {
        var tempSuites = cardSuitesInGame.ToList();

        foreach (var suite in suitesInArea)
        {
            tempSuites.Remove(suite);
        }
        UnavailableSuites = tempSuites;
        HeroesInAreaAllowence = suitesInArea;
    }

    public void AddPlayerStartCard(PlayerArea playerArea)
    {
        var playerDeck = FindObjectOfType<PlayerCardDeck>();
        if (!playerDeck) { return; }

        var startingHero = playerDeck.AvilableCards.CardStats.FirstOrDefault(
            q => q.cardType == CardType.Hero &&
            q.suite == CardSuite.Clubs);
        if (!startingHero) { return; }

        var firstCard = playerDeck.CreateCardBasedOnStats(startingHero);
        playerArea.AddCard(firstCard);
    }
}
