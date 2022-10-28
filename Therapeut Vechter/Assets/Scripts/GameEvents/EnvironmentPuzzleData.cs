using Exercises;
using UnityEngine;

namespace GameEvents
{
    [CreateAssetMenu(menuName = "Game Events/Environment Puzzle Data")]
    public class EnvironmentPuzzleData : BaseGameEvent
    {
        public GameEventData[] exerciseData;
    }
}
