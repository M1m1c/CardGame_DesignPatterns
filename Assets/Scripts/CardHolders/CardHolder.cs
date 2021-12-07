using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    public bool IsActive { get; set; } = false;

    protected float cardXOffset = 8.5f;
    protected float cardXStartPos = -17f;
    protected float[] cardXPosStarts = new float[] { 0f, -4.45f, -8.5f, -12.75f, -17f, -21.25f,};

    protected int currentHeldCount = 0;
    protected CardBase[] heldCards = new CardBase[5];

    public bool DoesHeldCardsContainType(CardType cardType)
    {
        return heldCards.FirstOrDefault(q => q != null && q.Type == cardType);
    }

    protected virtual void ReorganizeHeldCardPositions()
    {
        var pos = cardXStartPos;
        var xOffset = 0f;
        for (int i = 0; i < heldCards.Length; i++)
        {

            var card = heldCards[i];
            if (!card) { continue; }
            if (card.transform.parent != transform) 
            {
                card.transform.parent = transform;
            }

            card.transform.position = this.transform.position + new Vector3(pos + xOffset, 0f, 0f);           
            card.SlotedPosition = heldCards[i].transform.position;
            xOffset += cardXOffset;
        }
    }
}
