using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class SetSliderValueToText : MonoBehaviour
    {
        private TMP_Text valueText;
        [SerializeField] private GameObject sliderParent;

        private float sliderValue;

        private void Start()
        {
            valueText = GetComponent<TMP_Text>();
        }

        // Update is called once per frame
        void Update()
        {
            sliderValue = sliderParent.GetComponent<Slider>().value;
            valueText.text = Mathf.Round(sliderValue * 100).ToString();
        }
    }
}
