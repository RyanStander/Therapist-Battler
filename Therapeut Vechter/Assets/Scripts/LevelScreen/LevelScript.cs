using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LevelScreen
{
    public class LevelScript : MonoBehaviour
    {
        //Get Components of Prefab
        [SerializeField]private Image levelIcon;

        
        [SerializeField] private Button playButton;

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
        public bool LevelFinished;
        public string Name;
        
        private LevelData level;


        private bool hasCountedStar;

        //get data from SO
        public void LoadLevelData(LevelData levelData)
        {
            level = levelData;
            Name = level.LevelName;
            LevelFinished = level.FinishedLevel;
            backgroundImage = level.LevelBackgroundImage;
        }

        private void Start()
        {
            PositionAndImage();
            ActivateStars();
            ChangeText();
            PositionBackground();
            SetButtonAction();
            StartCoroutine(CountStars(0.05f));
        }

        private IEnumerator CountStars(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            //is level unlocked?
            totalStars = sceneStarCount.GetComponent<StarCountScript>().StarsInScene;

            if (totalStars >= level.StarRequirement && isUnlockedBool == false)
            {
                lockIcon.SetActive(false);
                isUnlockedBool = true;
            }

            hasCountedStar = true;
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
            if (level.StarCount >= 1)
            {
                starOne.SetActive(true);
            }

            if (level.StarCount >= 2)
            {
                starTwo.SetActive(true);
            }

            if (level.StarCount == 3)
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
            proTextNumber.text = level.LevelNumber.ToString();
        }

        private void PositionAndImage()
        {
            //sprite image
            levelIcon.sprite = level.SpriteIcon;
            //position
            var levelTransform = transform;
            levelTransform.localPosition = levelTransform.position + new Vector3(0, level.HeightValue, 0);
        }
        //put the background image of the levels at middle height of screen
        private void PositionBackground()
        {
            backgroundTransform.GetComponent<Image>().sprite = backgroundImage;
            backgroundWidth = GameObject.Find("LevelSpawner").GetComponent<GameObjectSpawn>().SpawnDistance;
            backgroundRectTransform.sizeDelta = new Vector2(backgroundWidth,Screen.height);
            backgroundRectTransform.position = new Vector3(backgroundRectTransform.position.x, 1, backgroundRectTransform.position.z);
        }

        private void SetButtonAction()
        {
            playButton.onClick.AddListener(LoadSceneWithSpecifiedLevel);
        }

        private void LoadSceneWithSpecifiedLevel()
        {
            GameData.Instance.currentLevel = level.GameEventDataHolderLevel;
            SceneManager.LoadScene(1);
        }
    }
}