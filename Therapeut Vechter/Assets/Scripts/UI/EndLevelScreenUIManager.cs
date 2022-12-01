using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EndLevelScreenUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject levelEndScreen;
        [SerializeField] private GameObject hud;
        [SerializeField] private GameObject levelEndCharacters;

        [SerializeField] private Slider scoreSlider;
        [SerializeField] private float scoreUpdateSpeed = 0.01f;
        private float currentDisplayScore;
        private float currentScore;
        private float maxScore;
        
        [Header("Stars")] [Header("Top Star")] [SerializeField]
        private Image topStarImage;

        [SerializeField] private Animation topStarAnimation;
        private bool topStarUnlocked;

        [Header("Middle Star")] [SerializeField]
        private Image middleStarImage;

        [SerializeField] private Animation middleStarAnimation;
        private bool middleStarUnlocked;

        [Header("Bottom Star")] [SerializeField]
        private Image bottomStarImage;

        [SerializeField] private Animation bottomStarAnimation;
        private bool bottomStarUnlocked;

        [Header("Star Sprites")] [SerializeField]
        private Sprite obtainedStarSprite;

        [SerializeField] private Sprite unobtainedStarSprite;

        private void Update()
        {
            LerpUpdateEnemyHealth();
            CheckForStarActivation();
        }

        private void Start()
        {
            topStarImage.sprite = unobtainedStarSprite;
            middleStarImage.sprite = unobtainedStarSprite;
            bottomStarImage.sprite = unobtainedStarSprite;
        }
        
        private void LerpUpdateEnemyHealth()
        {
            currentDisplayScore = Mathf.Lerp(currentDisplayScore, currentScore, scoreUpdateSpeed);

            scoreSlider.value = currentDisplayScore;
        }
        
        private void CheckForStarActivation()
        {
            //full stars
            if (currentDisplayScore > maxScore * 0.9f && !topStarUnlocked)
            {
                topStarImage.sprite = obtainedStarSprite;
                topStarAnimation.Play();
                topStarUnlocked = true;
            }

            //two stars
            if (currentDisplayScore > maxScore * 2 / 3 && !middleStarUnlocked)
            {
                middleStarImage.sprite = obtainedStarSprite;
                middleStarAnimation.Play();
                middleStarUnlocked = true;
            }

            //one star
            if (currentDisplayScore > maxScore * 1 / 3 && !bottomStarUnlocked)
            {
                bottomStarImage.sprite = obtainedStarSprite;
                bottomStarAnimation.Play();
                bottomStarUnlocked = true;
            }
        }

        #region OnEvents

        private void OnEnable()
        {
            EventManager.currentManager.Subscribe(EventType.EndLevel,OnLevelEnd);
        }

        private void OnDisable()
        {
            EventManager.currentManager.Unsubscribe(EventType.EndLevel,OnLevelEnd);
        }
        
        private void OnLevelEnd(EventData eventData)
        {
            if (eventData is EndLevel endLevel)
            {
                levelEndScreen.SetActive(true);
                levelEndCharacters.SetActive(true);
                hud.SetActive(false);

                currentScore = endLevel.PlayerScore;
                maxScore = endLevel.MaxScore;
                scoreSlider.maxValue = maxScore;
                scoreSlider.value = 0;
            }
            else
            {
                Debug.Log("The given Event type of EndLevel is not EventData type EndLevel, please ensure right event is being sent");
            }
        }

        #endregion
    }
}