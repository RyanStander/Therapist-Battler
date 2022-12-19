using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScoreUIManager : MonoBehaviour
    {
        [SerializeField] private Slider scoreSlider;
        [SerializeField] private float scoreUpdateSpeed = 1f;

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

        private float currentDisplayScore;
        private float currentScore;
        private float maxScore;

        private void OnEnable()
        {
            EventManager.currentManager.Subscribe(EventType.UpdateTotalScore, OnUpdateScore);
            EventManager.currentManager.Subscribe(EventType.SetupTotalScore, OnSetupScore);
        }

        private void OnDisable()
        {
            EventManager.currentManager.Unsubscribe(EventType.UpdateTotalScore, OnUpdateScore);
            EventManager.currentManager.Unsubscribe(EventType.SetupTotalScore, OnSetupScore);
        }

        private void Start()
        {
            topStarImage.sprite = unobtainedStarSprite;
            middleStarImage.sprite = unobtainedStarSprite;
            bottomStarImage.sprite = unobtainedStarSprite;
        }

        private void FixedUpdate()
        {
            LerpUpdateEnemyHealth();
            CheckForStarActivation();
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

        private void LerpUpdateEnemyHealth()
        {
            currentDisplayScore = Mathf.Lerp(currentDisplayScore, currentScore, scoreUpdateSpeed);

            scoreSlider.value = currentDisplayScore;
        }

        private void OnUpdateScore(EventData eventData)
        {
            if (eventData is UpdateTotalScore updateTotalScore)
            {
                currentScore += updateTotalScore.Score;
            }
        }

        private void OnSetupScore(EventData eventData)
        {
            if (eventData is SetupTotalScore setupTotalScore)
            {
                maxScore = setupTotalScore.MaxScore;
                scoreSlider.value = 0;
                scoreSlider.maxValue = maxScore;
            }
        }
    }
}