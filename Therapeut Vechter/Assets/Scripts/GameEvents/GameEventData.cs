using Exercises;
using FMODUnity;
using UnityEngine;

namespace GameEvents
{
    [System.Serializable]
    public class GameEventData
    {
        public PoseDataSet ExerciseToPerform;
        public EventReference VoiceLineToPlay;
        public Sprite SpriteToShow;
    }
}
