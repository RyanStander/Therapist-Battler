using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelFunctionality : MonoBehaviour
{
    public LevelSelect levelSelect;
    private Sprite spriteImage;
    private Image S_Image;
    private string levelString;
    private string levelNumber;
    private int starCount;
    private int heightValue;

    GameObject StarOne;
    GameObject StarTwo;
    GameObject StarThree;
    //namegetting
    public LevelSelect levelSO;
    string nameObject;
    string levelNumberStringRemove;
    string levelNumberString;
    string testString = "LevelSO 01";

    void Start()
    {
        //getScriptableObject
        levelSO = Resources.Load<LevelSelect>("LevelSO/" + testString);
        levelSelect = levelSO;
        //getnamenumber
        nameObject = this.ToString();
        levelNumberStringRemove = nameObject.Replace("Level", "LevelSO");
        levelNumberString = levelNumberStringRemove.Replace("(Clone) (levelFunctionality)", string.Empty);
        //position
        heightValue = levelSelect.HeightValue;
        transform.position = transform.position + new Vector3(0, heightValue, 0);
        
        //image
        spriteImage = levelSelect.Images;
        S_Image = GetComponent<Image>();

        S_Image.sprite = spriteImage;
        //text
        levelString = levelSelect.LevelName;
        levelNumber = levelSelect.LevelNumber.ToString();

        TMPro.TextMeshProUGUI ProText = transform.Find("LevelName").GetComponent<TMPro.TextMeshProUGUI>();
        TMPro.TextMeshProUGUI ProTextNumber = transform.Find("LevelNumber").GetComponent<TMPro.TextMeshProUGUI>();
        ProText.text = levelString;
        ProTextNumber.text = levelNumber;
        //stars
        StarOne = GameObject.Find("Star 1");
        StarTwo = GameObject.Find("Star 2");
        StarThree = GameObject.Find("Star 3");
        starCount = levelSelect.StarCount;

        StarOne.SetActive(false);
        StarTwo.SetActive(false);
        StarThree.SetActive(false);
        //stars activation
        if (starCount >= 1)
        {
            StarOne.SetActive(true);
        }
        if (starCount >= 2)
        {
            StarTwo.SetActive(true);
        }
        if (starCount == 3)
        {
            StarThree.SetActive(true);
        }
    }
    /*
    void Update()
    {
        //image
        S_Image.sprite = spriteImage;
        //text
        TMPro.TextMeshProUGUI ProText = transform.Find("LevelName").GetComponent<TMPro.TextMeshProUGUI>();
        TMPro.TextMeshProUGUI ProTextNumber = transform.Find("LevelNumber").GetComponent<TMPro.TextMeshProUGUI>();
        ProText.text = levelString;
        ProTextNumber.text = levelNumber;
    }
    */
}
