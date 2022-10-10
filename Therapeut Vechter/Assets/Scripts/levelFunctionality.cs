using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelFunctionality : MonoBehaviour
{
    private LevelSelect levelSelect;
    private Sprite spriteImage;
    private Image levelIcon;
    private string levelString;
    private string levelNumber;
    private int starCount;
    private int heightValue;

    private GameObject starOne;
    private GameObject starTwo;
    private GameObject starThree;
    //namegetting
    private string nameObject;
    private string levelNumberStringRemove;
    private string levelNumberString;
    private string testString = "LevelSO 01";

    void Start()
    {
        //getScriptableObject
        levelSelect = Resources.Load<LevelSelect>("LevelSO/" + testString);
        //getnamenumber
        nameObject = this.ToString();
        levelNumberStringRemove = nameObject.Replace("Level", "LevelSO");
        levelNumberString = levelNumberStringRemove.Replace("(Clone) (levelFunctionality)", string.Empty);
        //position
        heightValue = levelSelect.HeightValue;
        transform.position = transform.position + new Vector3(0, heightValue, 0);
        
        //image
        spriteImage = levelSelect.SpriteIcon;
        levelIcon = GetComponent<Image>();

        levelIcon.sprite = spriteImage;
        //text
        levelString = levelSelect.LevelName;
        levelNumber = levelSelect.LevelNumber.ToString();

        TMPro.TextMeshProUGUI ProText = transform.Find("LevelName").GetComponent<TMPro.TextMeshProUGUI>();
        TMPro.TextMeshProUGUI ProTextNumber = transform.Find("LevelNumber").GetComponent<TMPro.TextMeshProUGUI>();
        ProText.text = levelString;
        ProTextNumber.text = levelNumber;
        //stars
        starOne = GameObject.Find("Star 1");
        starTwo = GameObject.Find("Star 2");
        starThree = GameObject.Find("Star 3");
        starCount = levelSelect.StarCount;

        starOne.SetActive(false);
        starTwo.SetActive(false);
        starThree.SetActive(false);
        //stars activation
        if (starCount >= 1)
        {
            starOne.SetActive(true);
        }
        if (starCount >= 2)
        {
            starTwo.SetActive(true);
        }
        if (starCount == 3)
        {
            starThree.SetActive(true);
        }
    }
}
