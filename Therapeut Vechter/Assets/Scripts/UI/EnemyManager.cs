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
        [SerializeField] private Image enemyBackgroundImage;
        [SerializeField] private RectTransform enemyInstantiateLocation;
        [SerializeField] private Slider[] enemySliders;
        [SerializeField] private Color enemyDamageColor = Color.red;
        [SerializeField] private float colorUpdateSpeed = 1f;

        private float currentDisplayHealth;
        private float currentHealth;
        private float healthUpdateSpeed;

        private bool playDamageEffect;
        private Color defaultColor = Color.white;

        private EnemyVideoDataHolder enemyVideoDataHolder;


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

        private void FixedUpdate()
        {
            LerpUpdateEnemyHealth();

            PlayDamageEffect();
        }

        private void OnSetupEnemy(EventData eventData)
        {
            if (eventData is SetupEnemy setupEnemy)
            {
                if (setupEnemy.EnemyGameObject != null)
                {
                    enemyVideoDataHolder = Instantiate(setupEnemy.EnemyGameObject, enemyInstantiateLocation)
                        .GetComponent<EnemyVideoDataHolder>();
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

                if (enemyVideoDataHolder == null)
                    return;
                enemyVideoDataHolder.Animator.Play("EnemyImageHitEffect");

                enemyVideoDataHolder.RawImage.color = enemyDamageColor;
            }
            else
                Debug.LogError(
                    "Received EventType.DamageEnemy for EnemyManager but the EventData type is not of type DamageEnemy");
        }

        private void OnHideEnemy(EventData eventData)
        {
            if (eventData is HideEnemy)
            {
                foreach (var enemySlider in enemySliders)
                {
                    enemySlider.gameObject.SetActive(false);
                }

                enemyBackgroundImage.gameObject.SetActive(false);
                
                if (enemyVideoDataHolder == null)
                    return;
                Destroy(enemyVideoDataHolder.gameObject);
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

            if (enemyVideoDataHolder == null)
                return;
            Destroy(enemyVideoDataHolder.gameObject);
            var enemyColor = enemyVideoDataHolder.RawImage.color;

            var c = Color.Lerp(enemyColor, defaultColor, colorUpdateSpeed);

            enemyVideoDataHolder.RawImage.color = c;

            if (Math.Abs(enemyColor.r - defaultColor.r) < 0.01f && Math.Abs(enemyColor.g - defaultColor.g) < 0.01f &&
                Math.Abs(enemyColor.b - defaultColor.b) < 0.01f)
            {
                playDamageEffect = false;
            }
        }
    }
}