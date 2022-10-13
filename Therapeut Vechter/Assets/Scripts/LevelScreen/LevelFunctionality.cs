using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelScreen
{
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
        public string TestString = "LevelSO 01";
        //finished?
        public bool IsLevelFinished;

        private void Start()
        {
            //getScriptableObject
            levelSelect = Resources.Load<LevelSelect>("LevelSO/" + TestString);
            //getnamenumber
            nameObject =ToString();
            levelNumberStringRemove = nameObject.Replace("Level", "LevelSO");
            levelNumberString = levelNumberStringRemove.Replace("(Clone) (levelFunctionality)", string.Empty);
            //position
            heightValue = levelSelect.HeightValue;
            var levelFunctionalityTransform = transform;
            levelFunctionalityTransform.localPosition =
                levelFunctionalityTransform.position + new Vector3(0, heightValue, 0);

            //image
            spriteImage = levelSelect.SpriteIcon;
            levelIcon = GetComponent<Image>();

            levelIcon.sprite = spriteImage;
            //text
            levelString = levelSelect.LevelName;
            levelNumber = levelSelect.LevelNumber.ToString();

            var proText = transform.Find("LevelName").GetComponent<TextMeshProUGUI>();
            var proTextNumber = transform.Find("LevelNumber").GetComponent<TextMeshProUGUI>();
            proText.text = levelString;
            proTextNumber.text = levelNumber;
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
            //finished?
            IsLevelFinished = levelSelect.FinishedLevel;
        }
    }
}