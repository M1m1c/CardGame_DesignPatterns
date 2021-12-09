using System.Collections.Generic;

public class PlayerArea : CardArea
{
    public Subject<List<CardSuite>> OnUpdateAvailableSuites { get; private set; } = new Subject<List<CardSuite>>();

    public override void AddCard(CardBase card)
    {
        base.AddCard(card);
        SendCarSuitesOnField();
    }

    public override void RemoveCard(CardBase card)
    {
        base.RemoveCard(card);
        SendCarSuitesOnField();
    }

    private void SendCarSuitesOnField()
    {
        var cardSuitesOnField = new List<CardSuite>();
        foreach (var item in heldCards)
        {
            if (item == null) { continue; }
            if (item.Suite == CardSuite.None) { continue; }
            cardSuitesOnField.Add(item.Suite);
        }
        OnUpdateAvailableSuites.Notify(cardSuitesOnField);
    }
}
