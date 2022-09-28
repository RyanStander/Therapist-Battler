using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void OnValidate()
    {
        if (slider==null)
        {
            slider= GetComponent<Slider>();
        }
    }

    public void SetCurrentValue(float current)
    {
        slider.value = current;
    }

    public void SetValues(float current, float max)
    {
        slider.value = current;
        slider.maxValue = max;
    }
}
