﻿using Exercises;
using FMODUnity;
using UnityEngine.Serialization;

namespace GameEvents
{
    [System.Serializable]
    public class PlayerAttackSequence
    {
        public EventReference startingVoiceLine;
        [FormerlySerializedAs("exerciseName")] public EventReference randomVoiceLine;
        public bool advanceToNextAudioStageAtStartOfExerciseSet = false;
        public PoseDataSet playerAttack;
        public int timesToPerform=1;
    }
}