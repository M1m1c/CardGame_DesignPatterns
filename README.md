# CardGame_DesignPatterns
***By Ludvig Baummann Olsson***

An asymetric card game where the player tries to survive for as many rounds as possible against an enemy that creates new monster cards each round.
The player has two types of cards which they can draw:

`Hero cards`- Which the player can place on their area of the board. These function as the player's health and determine the type of action cards that can be drawn.

`Action cards`- These are the main ways the player can damage enemies with. Each suite funcitons differently:

Clubs- deal raw damage.

Hearts- can either deal damage or heal friendly hero cards.

Diamonds- performs a modulo operation on the enemy health.

Spades- splits their damage between the enemies randomly.

## Programming Patterns

### Factory Pattern
The card decks in the game use the factory pattern to produce new cards whenever they are requested.\
The scripts can be found in the folder `Assets/Scripts/Cards/Decks`,
and in the scripts `PlayerCardDeck.cs` `AICardDeck.cs`.\
These scripts are used in the prefabs PlayerCardDeck and AiCardDeck found in the prefabs folder. 

### Strategy Pattern
The cards in the game use the Strategy pattern as described in this video\
https://www.youtube.com/watch?v=yjZsAl13trk

This pattern is used so the cards can each individually produce different effects when selected, de-selected,\
played or when they leave the game board.\
The actions use a base class that they inherit from and override to give the action its own functionallity.\
Each card holds an arrays of actions for each way they can react.
Giving a card an action, is done by setting the actions in a CardStats class scriptable object.
The object is then used when creating a new card to pass on the actions onto the instantiated card.

The card script that uses the actions can be found in `Assets/Scripts/Cards`, specifically `CardBase.cs`.\
The actions themselves can be found in `Assets/Scripts/Cards/CardActions`, where `CardAction.cs` is the base acion class.\
The card stats script can be found in `Assets/Scripts/Cards/CardStats`, `CardStatsBase.cs`.

The actions inherit from scriptable object and for each action a corresponding scriptable object asset can be found in `Assets/ScriptableObjects/CardActions`.\
The card stat scriptable objects where the actions for each card are set can be found in any subfolder under `Assets/ScriptableObjects/CardStats`.

### Singleton
The singleton pattern is used once in the game and that is for the `GameMaster.cs` there can only be one game master in the game.
The script can be found in the folder `Assets/Scripts`. At the top of the class the static variable "Instance" of type game master is declared, it is then set in the Awake() method.

