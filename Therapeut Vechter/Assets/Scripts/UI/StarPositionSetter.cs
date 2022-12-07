using System;
using UnityEngine;

namespace UI
{
    public class StarPositionSetter : MonoBehaviour
    {
        [SerializeField] private RectTransform scoreSlider;

        [SerializeField] private RectTransform bottomStar;
        [SerializeField] private RectTransform middleStar;
        [SerializeField] private RectTransform topStar;

        private void OnValidate()
        {
            var height = scoreSlider.rect.width;

            var bottomStarRect = bottomStar.localPosition;
            bottomStarRect.x = height * 1 / 3 - height / 2;
            bottomStar.localPosition = bottomStarRect;

            var middleStarRect = middleStar.localPosition;
            middleStarRect.x = height * 2 / 3 - height / 2;
            middleStar.localPosition = middleStarRect;

            var topStarRect = topStar.localPosition;
            topStarRect.x = height - height / 2;
            topStar.localPosition = topStarRect;
        }
    }
}