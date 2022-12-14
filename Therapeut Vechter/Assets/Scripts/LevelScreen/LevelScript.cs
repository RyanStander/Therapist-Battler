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
        [SerializeField] private Image levelIcon;

        [SerializeField] private Button playButton;

        [SerializeField] private RectTransform maskRectTransform;
        [SerializeField] private RawImage backgroundRawImage;
        [SerializeField] private Canvas maskCanvas;
        [SerializeField] private GameObject exerciseDisplayContent;
        [SerializeField] private GameObject exerciseButton;

        [Header("Level Load Stats")] [Tooltip("How fast the mask moves to its position")] [SerializeField]
        private float positionChangeScalar = 0.001f;

        [SerializeField] [Tooltip("How fast the mask moves as an extra modifier when its close to the point")]
        private float positionChangeSpeed = 0.01f;

        [SerializeField] private float imageScaleSpeed = 0.00015f;

        [SerializeField] private float maskWidthIncreaseSpeed = 18;

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
        [HideInInspector] public bool LevelFinished;
        [HideInInspector] public string Name;

        private LevelData level;

        private bool startLoadEffect;

        #region Runtime

        private void Start()
        {
            PositionAndImage();
            ActivateStars();
            ChangeText();
            PositionBackground();
            SetButtonAction();
            SpawnExerciseButtons();
            StartCoroutine(CountStars(0.05f));
        }

        private void FixedUpdate()
        {
            LoadEffect();
        }

        #endregion

        //get data from SO
        public void LoadLevelData(LevelData levelData)
        {
            level = levelData;
            Name = level.LevelName;
            LevelFinished = level.FinishedLevel;
        }
        
        private void PositionAndImage()
        {
            //sprite image
            levelIcon.sprite = level.SpriteIcon;
            //position
            var levelTransform = transform;
            levelTransform.localPosition = levelTransform.position + new Vector3(0, level.HeightValue, 0);
        }

        #region Stars

        private IEnumerator CountStars(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            //is level unlocked?
            totalStars = sceneStarCount.GetComponent<StarCountScript>().StarsInScene;

            if (totalStars < level.StarRequirement || isUnlockedBool) yield break;
            lockIcon.SetActive(false);
            isUnlockedBool = true;
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

        #endregion

        private void ChangeText()
        {
            //text
            var proText = transform.Find("LevelName").GetComponent<TextMeshProUGUI>();
            var proTextNumber = transform.Find("LevelNumber").GetComponent<TextMeshProUGUI>();
            proText.text = Name;
            proTextNumber.text = level.LevelNumber.ToString();
        }

        #region Play functionality

        private void SetButtonAction()
        {
            playButton.onClick.AddListener(LoadSceneWithSpecifiedLevel);
        }

        private void LoadSceneWithSpecifiedLevel()
        {
            maskCanvas.sortingOrder = -2;
            startLoadEffect = true;
            StartCoroutine(StartSceneLoad(2.5f));
        }

        private void LoadEffect()
        {
            if (!startLoadEffect)
                return;

            var rawImageTransform = backgroundRawImage.transform;
            var maskPosition = maskRectTransform.position;

            //Increase scale slightly
            rawImageTransform.localScale *= 1 + imageScaleSpeed;

            //Increase width slightly
            maskRectTransform.sizeDelta += new Vector2(maskWidthIncreaseSpeed, 0);

            //move towards center
            if (maskRectTransform.position.x < 0)
            {
                maskPosition += new Vector3(-maskPosition.x * positionChangeScalar + positionChangeSpeed, 0, 0);
                maskRectTransform.position = maskPosition;
            }
            else
            {
                maskPosition -= new Vector3(maskPosition.x * positionChangeScalar + positionChangeSpeed, 0, 0);
                maskRectTransform.position = maskPosition;
            }
        }

        private void SpawnExerciseButtons()
        {
            foreach (var exercise in level.GameEventDataHolderLevel.exercisesInLevel)
            {
                var obj = Instantiate(exerciseButton, exerciseDisplayContent.transform);
                var excludeExercise = obj.GetComponent<UI.ExcludeExercise>();
                excludeExercise.SetExerciseToExclude(exercise);
            }
        }

        //put the background image of the levels at middle height of screen
        private void PositionBackground()
        {
            backgroundRawImage.texture = level.LevelBackgroundImage;
            maskRectTransform.position =
                new Vector3(maskRectTransform.position.x, 1, maskRectTransform.position.z);
        }

        private IEnumerator StartSceneLoad(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);

            GameData.Instance.currentLevel = level.GameEventDataHolderLevel;
            SceneManager.LoadScene(1);
        }

        #endregion
    }
}