using UnityEngine;

public class CardPlayable : CardBase
{
    public TextMesh FirstLetterName;
    public TextMesh RestName;
    public TextMesh HealthText;
    public Renderer FrontImage;

    private Material frontImageMat;

    public override void Setup(CardStatsBase cardStats, OwnerEnum ownerEnum)
    {
        base.Setup(cardStats, ownerEnum);

        if (cardStats.DisplayName != "")
        {
            var firstLetter = cardStats.DisplayName.Substring(0, 1).ToUpper();
            var restOfText = cardStats.DisplayName.Substring(1).ToLower();

            string FormatedText = "";
            for (int i = 0; i < restOfText.Length; i++)
            {
                FormatedText += restOfText[i] + "\n";
            }

            FirstLetterName.text = firstLetter;
            RestName.text = FormatedText;
        }

        HealthText.text = "" + cardValue;
        frontImageMat = new Material(FrontImage.material);
        frontImageMat.mainTexture = cardStats.Image;
        FrontImage.material = frontImageMat;
    }

    public void ChangeCardValue(int changeValue)
    {
        cardValue = Mathf.Clamp(cardValue + changeValue, 0, 30);

        HealthText.text = "" + cardValue;

        if (cardValue <= 0)
        {
            TryDoLeaveActions(transform.parent.gameObject);
        }
    }
}
