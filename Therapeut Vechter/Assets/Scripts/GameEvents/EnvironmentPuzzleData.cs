using UnityEngine;

namespace GameEvents
{
    [CreateAssetMenu(menuName = "Game Events/Environment Puzzle Data", order = 1)]
    public class EnvironmentPuzzleData : ScriptableObject
    {
        public AnimationClip[] exerciseToPerform;
        public AudioClip voiceLineToPlay;
    }
}
