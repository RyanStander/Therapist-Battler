using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelScreen
{
    public class LevelScript : MonoBehaviour
    {
        //Get Components of Prefab
        [SerializeField]private Image levelIcon;

        //Stars
        private GameObject starOne;
        private GameObject starTwo;

        private GameObject starThree;

        //unlocked?
        private GameObject sceneStarCount;
        private bool isUnlockedBool;
        private GameObject lockIcon;
        private int totalStars;
        //background
        private Sprite backgroundImage;
        [SerializeField] private RectTransform backgroundRectTransform;
        [SerializeField] private Image backgroundTransform;
        private int backgroundWidth;


        //Saved data from SO
        public LevelData level;
        public string Name;
        public int levelNumber;
        public int SpawnHeight;
        public Sprite levelSprite;
        public int Stars;
        public int StarsRequired;
        public bool LevelFinished;


        //get data from SO
        public void LoadLevelData(LevelData levelData)
        {
            level = levelData;
            Name = level.LevelName;
            levelNumber = level.LevelNumber;
            SpawnHeight = level.HeightValue;
            levelSprite = level.SpriteIcon;
            Stars = level.StarCount;
            StarsRequired = level.StarRequirement;
            LevelFinished = level.FinishedLevel;
            backgroundImage = level.LevelBackgroundImage;
        }

        private void Start()
        {
            PositionAndImage();
            ActivateStars();
            ChangeText();
            PositionBackground();
        }

        private void Update()
        {
            //is level unlocked?
            totalStars = sceneStarCount.GetComponent<StarCountScript>().StarsInScene;

            if (totalStars >= StarsRequired && isUnlockedBool == false)
            {
                lockIcon.SetActive(false);
                isUnlockedBool = true;
            }
        }

        private void ActivateStars()
        {
            //Star amount getting
            sceneStarCount = GameObject.Find("StarAmount");
            lockIcon = transform.Find("LockIcon").gameObject;
            //stars
            starOne = GameObject.Find("Star 1");
            starTwo = GameObject.Find("Star 2");
            starThree = GameObject.Find("Star 3");
            starOne.SetActive(false);
            starTwo.SetActive(false);
            starThree.SetActive(false);
            //stars activation
            if (Stars >= 1)
            {
                starOne.SetActive(true);
            }

            if (Stars >= 2)
            {
                starTwo.SetActive(true);
            }

            if (Stars == 3)
            {
                starThree.SetActive(true);
            }
        }

        private void ChangeText()
        {
            //text
            var proText = transform.Find("LevelName").GetComponent<TextMeshProUGUI>();
            var proTextNumber = transform.Find("LevelNumber").GetComponent<TextMeshProUGUI>();
            proText.text = Name;
            proTextNumber.text = levelNumber.ToString();
        }

        private void PositionAndImage()
        {
            //sprite image
            levelIcon.sprite = levelSprite;
            //position
            var levelTransform = transform;
            levelTransform.localPosition = levelTransform.position + new Vector3(0, SpawnHeight, 0);
        }
        //put the background image of the levels at middle height of screen
        private void PositionBackground()
        {
            backgroundTransform.GetComponent<Image>().sprite = backgroundImage;
            backgroundWidth = GameObject.Find("LevelSpawner").GetComponent<GameObjectSpawn>().SpawnDistance;
            backgroundRectTransform.sizeDelta = new Vector2(backgroundWidth,Screen.height);
            backgroundRectTransform.position = new Vector3(backgroundRectTransform.position.x, 1, backgroundRectTransform.position.z);
        }
    }
}