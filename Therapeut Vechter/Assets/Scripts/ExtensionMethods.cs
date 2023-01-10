    using System.Collections.Generic;
    using Exercises;
    using FMODUnity;
    using GameEvents;
    using UnityEngine;

    public static class ExtensionMethods
    {
        /// <summary>
        /// Returns a list of all exercises included in a list of events
        /// </summary>
        public static List<PoseDataSet> GetAllExercises(List<BaseGameEvent> gameEvents)
        {
            var exercisesInLevel = new List<PoseDataSet>();
            foreach (var gameEvent in gameEvents)
            {
                switch (gameEvent)
                {
                    //we dont care for dialogue events
                    case DialogueData:
                        continue;
                    case EnvironmentPuzzleData environmentPuzzleData:
                    {
                        foreach (var exerciseData in environmentPuzzleData.exerciseData)
                        {
                            if (exercisesInLevel.Contains(exerciseData.ExerciseToPerform))
                                continue;
                            exercisesInLevel.Add(exerciseData.ExerciseToPerform);
                        }

                        break;
                    }
                    case FightingData fightingData:
                    {
                        foreach (var playerAttackSequence in fightingData.playerAttackSequence)
                        {
                            if (exercisesInLevel.Contains(playerAttackSequence.playerAttack))
                                continue;
                            exercisesInLevel.Add(playerAttackSequence.playerAttack);
                        }

                        break;
                    }
                }
            }

            return exercisesInLevel;
        }

        /// <summary>
        /// Returns a list of all exercises included an event
        /// </summary>
        public static List<PoseDataSet> GetAllExercises(FightingData fightingData)
        {
            //Get all the exercises 
            var fightingDataExercises = new List<PoseDataSet>();
        
            foreach (var playerAttackSequence in fightingData.playerAttackSequence)
            {
                if (fightingDataExercises.Contains(playerAttackSequence.playerAttack))
                    continue;
                fightingDataExercises.Add(playerAttackSequence.playerAttack);
            }

            return fightingDataExercises;
        }
        
        /// <summary>
        /// Returns a list of all exercises included an event
        /// </summary>
        public static List<PoseDataSet> GetAllExercises(EnvironmentPuzzleData environmentPuzzleData)
        {
            //Get all the exercises 
            var puzzleExercises = new List<PoseDataSet>();
        
            foreach (var exerciseData in environmentPuzzleData.exerciseData)
            {
                if (puzzleExercises.Contains(exerciseData.ExerciseToPerform))
                    continue;
                puzzleExercises.Add(exerciseData.ExerciseToPerform);
            }

            return puzzleExercises;
        }
        
        public static bool IsPathValid(string eventPath)
        {
            RuntimeManager.StudioSystem.getEvent(eventPath, out var eventDescription);
            
            if (eventDescription.isValid())
            {
                return true;
            }

            Debug.LogWarning("The path: '" + eventPath + "' is not valid. Sound will not be played");
            return false;
        }
    }