using Exercises;
using FMODUnity;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameEvents
{
    [System.Serializable]
    public class PlayerAttackSequence
    {
        public EventReference startingVoiceLine;
        [FormerlySerializedAs("exerciseName")] public EventReference randomVoiceLine;
        [Tooltip("If true it will advance to the next audio stage of the music at the start of this.")]
        public bool advanceToNextAudioStageAtStartOfExerciseSet = false;
        [Tooltip("If true it will advance to the next audio stage of the music at the end of this.")]
        public bool advanceToNextAudioStageAtEndOfExerciseSet = false;
        public PoseDataSet playerAttack;
        public int timesToPerform=1;
    }
}