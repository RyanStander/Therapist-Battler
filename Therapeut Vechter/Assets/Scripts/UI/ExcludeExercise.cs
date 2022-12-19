using System;
using Exercises;
using LevelScreen;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ExcludeExercise : MonoBehaviour
    {
        private PoseDataSet exerciseToExclude;
        public void ExcludeExerciseFromLevel()
        {
            var exerciseButtonScript = GetComponentInChildren<ExerciseButtonScript>();
            if (exerciseButtonScript.ExerciseActive)
            {
                GameData.Instance.exercisesToExclude.Remove(exerciseToExclude);
            }
            else
            {
                GameData.Instance.exercisesToExclude.Add(exerciseToExclude);
            }
            
            
        }

        public void SetExerciseToExclude(PoseDataSet exercise)
        {
            exerciseToExclude = exercise;
            
            var toolTip = GetComponent<ToolTip>();
            //If the exerciseName field is empty, set its name to be that of the object name
            toolTip.message = exercise.exerciseName=="" ? exerciseToExclude.name : exerciseToExclude.exerciseName;
            
            var icon = GetComponent<Image>();
            if (exerciseToExclude.exerciseIcon!=null)
                icon.sprite = exerciseToExclude.exerciseIcon;
        }
    }
}