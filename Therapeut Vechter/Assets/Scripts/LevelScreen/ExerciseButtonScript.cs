using UnityEngine;
using UnityEngine.UI;

namespace LevelScreen
{
    public class ExerciseButtonScript : MonoBehaviour
    {
        [SerializeField] private Color inactiveColor;
        [SerializeField] private Color activeColor;
        [SerializeField] private Image targetObject;
        //public for later if you need to know if exersize is active
        public bool ExerciseActive = true;

        public void ChangeColor()
        {
            //changing color and bool of exercise objects to be "active" and "inactive"
            if (ExerciseActive)
            {
                targetObject.color = inactiveColor;
                ExerciseActive = false;
            }
            else
            {
                targetObject.color = activeColor;
                ExerciseActive = true;
            }
        }
    }
}