
using UnityEngine;

public class HeroCard : CardBase
{
    public TextMesh FirstLetterName;
    public TextMesh RestName;
    public TextMesh HealthText;
    public Renderer FrontImage;

    private Material frontImageMat;

    public override void Setup(CardStatsBase cardStats)
    {
        base.Setup(cardStats);
        var firstLetter = name.Substring(0, 1).ToUpper();
        var restOfText = name.Substring(1).ToLower();
        FirstLetterName.text = firstLetter;
        RestName.text = restOfText;
        HealthText.text = "" + cardValue;
        frontImageMat.mainTexture = cardStats.Image;
        FrontImage.material=frontImageMat;
    }

    // Start is called before the first frame update
    void Start()
    {
        frontImageMat = new Material(FrontImage.material);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
