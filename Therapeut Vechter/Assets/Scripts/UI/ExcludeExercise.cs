using Exercises;
using UnityEngine;

namespace UI
{
    public class ExcludeExercise : MonoBehaviour
    {
        private PoseDataSet exerciseToExclude;
        public void SendExcludeExerciseEvent()
        {
            EventManager.currentManager.AddEvent(new global::ExcludeExercise(exerciseToExclude));
        }

        public void SetExerciseToExclude(PoseDataSet exercise)
        {
            exerciseToExclude = exercise;
        }
    }
}