using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelScreen
{
    public class LevelScript : MonoBehaviour
    {
        //Get Components of Prefab
        private Image levelIcon;
        //Stars
        private GameObject starOne;
        private GameObject starTwo;
        private GameObject starThree;
        //unlocked?
        private GameObject sceneStarCount;
        private bool isUnlockedBool;
        private GameObject lockIcon;
        private int totalStars;


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
        public void LoadLevelData (LevelData levelData )
        {
            level = levelData;
            Name = level.LevelName;
            levelNumber = level.LevelNumber;
            SpawnHeight = level.HeightValue;
            levelSprite = level.SpriteIcon;
            Stars = level.StarCount;
            StarsRequired = level.StarRequirement;
            LevelFinished = level.FinishedLevel;
        }

        private void Start()
        {
            //Star amount getting
            sceneStarCount = GameObject.Find("StarAmount");
            lockIcon = transform.Find("LockIcon").gameObject;
            //sprite image
            levelIcon = GetComponent<Image>();
            levelIcon.sprite = levelSprite;
            //text
            var proText = transform.Find("LevelName").GetComponent<TextMeshProUGUI>();
            var proTextNumber = transform.Find("LevelNumber").GetComponent<TextMeshProUGUI>();
            proText.text = Name;
            proTextNumber.text = levelNumber.ToString();
            
            //position
            var levelTransform = transform;
            levelTransform.localPosition = levelTransform.position + new Vector3(0, SpawnHeight, 0);
            
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
        private void Update()
        {
            //is level unlocked?
            totalStars = sceneStarCount.GetComponent<StarCountScript>().starsInScene;
            
            if(totalStars >= StarsRequired && isUnlockedBool ==false)
            {
                lockIcon.SetActive(false);
                isUnlockedBool = true;
            }
        }
    }
}
