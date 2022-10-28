using Exercises;
using UnityEngine;

namespace GameEvents
{
    [System.Serializable]
    public class GameEventData
    {
        public PoseDataSet ExerciseToPerform;
        public AudioClip VoiceLineToPlay;
        public Sprite SpriteToShow;
    }
}
