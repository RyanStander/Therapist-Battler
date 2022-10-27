using Exercises;
using UnityEngine;

namespace GameEvents
{
    [CreateAssetMenu(menuName = "Game Events/Environment Puzzle Data")]
    public class EnvironmentPuzzleData : BaseGameEvent
    {
        public GameEventData[] exerciseData;
    }

    [System.Serializable]
    public class GameEventData
    {
    public PoseDataSet ExerciseToPerform;
    public AudioClip VoiceLineToPlay;
    public Sprite SpriteToShow;
    }
}
