using UnityEngine;

public abstract class CardArea : CardHolder
{
    [SerializeField]
    private OwnerEnum owner;
    public OwnerEnum Owner { get { return owner; } }

    public Subject<CardHolder> OnTurnEnd { get; private set; } = new Subject<CardHolder>();

    public CardArea()
    {
        heldCards = new CardBase[6];
    }

    public override void RemoveCard(CardBase card)
    {
        base.RemoveCard(card);

        if (currentHeldCount <= 0)
        {
            OnTurnEnd.Notify(this);
        }
    }

   

}
