using UnityEngine;
using UnityEngine.UI;

namespace LevelScreen
{
    public class ExerciseButtonScript : MonoBehaviour
    {
        [SerializeField] private Color inactiveColor;
        [SerializeField] private Color activeColor;
        [SerializeField] private Image targetObject;
        public bool ExerciseActive = true;
        public void ChangeColor()
        {
        
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
