using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ComboUIManager : MonoBehaviour
    {
        [SerializeField] private Slider comboSlider;

        [SerializeField] private TextMeshProUGUI comboText;
        [SerializeField] private Animator comboTextAnimator;

        private bool isComboActive;
        private float comboTimeStamp;
        private int comboCount;

        private void OnEnable()
        {
            EventManager.currentManager.Subscribe(EventType.UpdateComboScore, OnUpdateComboScore);
        }

        private void OnDisable()
        {
            EventManager.currentManager.Unsubscribe(EventType.UpdateComboScore, OnUpdateComboScore);
        }

        private void OnUpdateComboScore(EventData eventData)
        {
            if (eventData is UpdateComboScore updateComboScore)
            {
                isComboActive = updateComboScore.EnableCombo;

                //Enable/Disable the combo display
                comboText.gameObject.SetActive(isComboActive);
                comboSlider.gameObject.SetActive(isComboActive);

                if (!isComboActive)
                    return;

                //Set the combo count
                comboCount = updateComboScore.ComboCount;
                comboText.text = "x" + comboCount;

                if (comboCount == 1)
                    comboTextAnimator.speed = 0;
                else
                    comboTextAnimator.speed = 1 + comboCount / 10;

                //Set the combo timer
                comboSlider.maxValue = updateComboScore.ComboTimer;
                comboSlider.value = updateComboScore.ComboTimer;
                comboTimeStamp = Time.time + updateComboScore.ComboTimer;
            }
            else
                Debug.LogError(
                    "Received EventType.UpdateComboScore for ComboUIManager but the EventData type is not of type UpdateComboScore");
        }

        private void FixedUpdate()
        {
            HandleComboTimer();
        }

        private void HandleComboTimer()
        {
            comboSlider.value = comboTimeStamp - Time.time;
        }
    }
}