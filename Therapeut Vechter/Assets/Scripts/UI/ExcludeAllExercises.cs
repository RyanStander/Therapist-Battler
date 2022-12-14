using LevelScreen;
using UnityEngine;

namespace UI
{
    public class ExcludeAllExercises : MonoBehaviour
    {
        
        public void ExcludeExercises()
        {
            GameData.Instance.exercisesToExclude.Clear();
            
            //We want to reset the color of the exercise button
            var exerciseButtons = GetComponentsInChildren<ExerciseButtonScript>();
            foreach (var exerciseButton in exerciseButtons)
            {
                exerciseButton.ExerciseActive = false;
                exerciseButton.ChangeColor();
            }
        }
    }
}