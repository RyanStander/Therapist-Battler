using UnityEngine;

namespace GameEvents
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Game Events/Puzzle Event")]
    public class EnvironmentPuzzleData : BaseGameEvent
    {
        [Header("Puzzle Specific")]
        public GameEventData[] exerciseData;
    }
}
