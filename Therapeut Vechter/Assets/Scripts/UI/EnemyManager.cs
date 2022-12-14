using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Manages the enemy displayed on screen
    /// </summary>
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private RawImage enemyImage;
        [SerializeField] private Image enemyBackgroundImage;
        [SerializeField] private Animator enemyImageAnimator;
        [SerializeField] private Slider[] enemySliders;
        [SerializeField] private Color enemyDamageColor = Color.red;
        [SerializeField] private float colorUpdateSpeed = 1f;

        private float currentDisplayHealth;
        private float currentHealth;
        private float healthUpdateSpeed;

        private bool playDamageEffect;
        private Color defaultColor;


        private void OnEnable()
        {
            EventManager.currentManager.Subscribe(EventType.SetupEnemy, OnSetupEnemy);
            EventManager.currentManager.Subscribe(EventType.DamageEnemyVisuals, OnDamageEnemy);
            EventManager.currentManager.Subscribe(EventType.HideEnemy, OnHideEnemy);
        }

        private void OnDisable()
        {
            EventManager.currentManager.Unsubscribe(EventType.SetupEnemy, OnSetupEnemy);
            EventManager.currentManager.Unsubscribe(EventType.DamageEnemyVisuals, OnDamageEnemy);
            EventManager.currentManager.Unsubscribe(EventType.HideEnemy, OnHideEnemy);
        }

        private void Start()
        {
            defaultColor = enemyImage.color;
        }

        private void FixedUpdate()
        {
            LerpUpdateEnemyHealth();

            PlayDamageEffect();
        }

        private void OnSetupEnemy(EventData eventData)
        {
            if (eventData is SetupEnemy setupEnemy)
            {
                if (setupEnemy.EnemySprite!=null)
                {
                    enemyImage.texture = setupEnemy.EnemySprite;
                    enemyImage.gameObject.SetActive(false);
                    enemyBackgroundImage.gameObject.SetActive(false);
                }
                
                currentDisplayHealth = setupEnemy.EnemyHealth;
                currentHealth = currentDisplayHealth;
                foreach (var enemySlider in enemySliders)
                {
                    enemySlider.maxValue = currentDisplayHealth;
                    enemySlider.value = currentDisplayHealth;   
                    enemySlider.gameObject.SetActive(true);
                }
                healthUpdateSpeed = setupEnemy.EnemyHealthUpdateSpeed;
                
                enemyBackgroundImage.gameObject.SetActive(true);
                enemyImage.gameObject.SetActive(true);
            }
            else
                Debug.LogError(
                    "Received EventType.SetupEnemy for EnemyManager but the EventData type is not of type SetupEnemy");
        }

        private void OnDamageEnemy(EventData eventData)
        {
            if (eventData is DamageEnemyVisuals damageEnemy)
            {
                currentHealth -= damageEnemy.DamageToTake;

                playDamageEffect = true;
                
                enemyImageAnimator.Play("EnemyImageHitEffect");

                enemyImage.color = enemyDamageColor;
            }
            else
                Debug.LogError(
                    "Received EventType.DamageEnemy for EnemyManager but the EventData type is not of type DamageEnemy");
        }

        private void OnHideEnemy(EventData eventData)
        {
            if (eventData is HideEnemy)
            {
                enemyImage.gameObject.SetActive(false);
                foreach (var enemySlider in enemySliders)
                {
                    enemySlider.gameObject.SetActive(false);
                }
                enemyBackgroundImage.gameObject.SetActive(false);
            }
            else
                Debug.LogError(
                    "Received EventType.HideEnemy for EnemyManager but the EventData type is not of type HideEnemy");
        }

        private void LerpUpdateEnemyHealth()
        {
            currentDisplayHealth = Mathf.Lerp(currentDisplayHealth, currentHealth, healthUpdateSpeed);

            
            foreach (var enemySlider in enemySliders)
            {
                enemySlider.value = currentDisplayHealth;
            }
        }

        private void PlayDamageEffect()
        {
            if (!playDamageEffect)
                return;


            var enemyColor = enemyImage.color;

            var c = Color.Lerp(enemyColor, defaultColor, colorUpdateSpeed);

            enemyImage.color = c;

            if (Math.Abs(enemyColor.r - defaultColor.r) < 0.01f && Math.Abs(enemyColor.g - defaultColor.g) < 0.01f &&
                Math.Abs(enemyColor.b - defaultColor.b) < 0.01f)
            {
                playDamageEffect = false;
            }
        }
    }
}