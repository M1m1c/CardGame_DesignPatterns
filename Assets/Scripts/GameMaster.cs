using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

    }

    private void Start()
    {

        var playerDeck = FindObjectOfType<PlayerCardDeck>();
        if (!playerDeck) { return; }

        var startingHero = playerDeck.AvilableCards.CardStats.FirstOrDefault(
            q => q.cardType == CardType.Hero &&
            q.suite == CardSuite.Clubs);
        if (!startingHero) { return; }

        var playAreas = FindObjectsOfType<PlayArea>();
        var playerArea = playAreas.FirstOrDefault(q => q.Owner == OwnerEnum.Player);
        if (!playerArea) { return; }

        var firstCard = playerDeck.CreateCardBasedOnStats(startingHero);
        playerArea.AddCard(firstCard);

    }

    //TODO use Observer pattern to look at the play areas and the player hand.
    //use state pattern to deal with whos turn it is.
    //If player cant play any more cards, transition AI turn.
    //If AI turn is done, go back to player turn.
    //If player area is empty fo cards the player loses the game
    //If enemy area is empty then the player won a round and can continue to the next round.
    // if the player plays a hero card to tehir play area,
    // add its suite to the available suites for the players action card draw.
}
