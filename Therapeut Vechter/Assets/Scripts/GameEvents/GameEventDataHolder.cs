using System;
using System.Collections.Generic;
using Exercises;
using UnityEngine;

namespace GameEvents
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Game Events/Game Event Data Holder",order = 1)]
    public class GameEventDataHolder : ScriptableObject
    {
        public Sprite startingBackground;

        [Range(1,3)]public int currentRecoveryStage=3;
        
        public BaseGameEvent[] gameEvents;

        public List<PoseDataSet> exercisesInLevel = new();

        private void OnValidate()
        {
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
        }
    }
}
