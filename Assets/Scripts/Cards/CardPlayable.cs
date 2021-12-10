using UnityEngine;

public class CardPlayable : CardBase
{
    public TextMesh FirstLetterName;
    public TextMesh RestName;
    public TextMesh HealthText;
    public Renderer FrontImage;
    public Renderer[] suiteIcons;

    private Material frontImageMat;
    private Material suiteIconMat;

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
                var add = cardStats.ShouldFormatText ? "\n" : "";
                FormatedText += restOfText[i] + add;
            }

            FirstLetterName.text = firstLetter;
            RestName.text = FormatedText;
        }

        HealthText.text = "" + cardValue;
       
        if (cardStats.Image == null)
        {
            FrontImage.enabled = false;
        }
        else
        {
            frontImageMat = new Material(FrontImage.material);
            frontImageMat.mainTexture = cardStats.Image;
            FrontImage.material = frontImageMat;
        }
        
        suiteIconMat = new Material(FrontImage.material);
        suiteIconMat.mainTexture = cardStats.SuiteIcon;
        foreach (var rend in suiteIcons)
        {
            if (!rend) { continue; }
            

            if (cardStats.SuiteIcon == null)
            {
                rend.enabled = false;
            }
            else
            {
                rend.material = suiteIconMat;
            }
        }

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
