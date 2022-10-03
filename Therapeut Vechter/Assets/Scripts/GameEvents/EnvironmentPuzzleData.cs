using UnityEngine;

namespace GameEvents
{
    [CreateAssetMenu(menuName = "Game Events/Environment Puzzle Data")]
    public class EnvironmentPuzzleData : BaseGameEvent
    {
        public AnimationClip[] exerciseToPerform;
        public AudioClip voiceLineToPlay;
    }
}
