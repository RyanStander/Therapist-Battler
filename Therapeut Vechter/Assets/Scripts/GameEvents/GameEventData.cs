using Exercises;
using FMODUnity;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameEvents
{
    [System.Serializable]
    public class GameEventData
    {
        public int timesToPerform=1;
        [Tooltip("If true it will advance to the next audio stage of the music.")]
        public bool advanceToNextAudioStageAtStartOfExerciseSet = false;
        public PoseDataSet ExerciseToPerform;
        public EventReference StartingVoiceLineToPlay;
        [FormerlySerializedAs("VoiceLineToPlay")] public EventReference RandomVoiceLineToPlay;
        public Sprite SpriteToShow;
    }
}
