using Exercises;
using UnityEngine;

namespace GameEvents
{
    [CreateAssetMenu(menuName = "Game Events/Puzzle Event")]
    public class EnvironmentPuzzleData : BaseGameEvent
    {
        [Header("Puzzle Specific")]
        public GameEventData[] exerciseData;
    }
}
