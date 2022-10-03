using UnityEngine;

namespace GameEvents
{
    [CreateAssetMenu(menuName = "Game Events/Game Event Data Holder",order = 1)]
    public class GameEventDataHolder : ScriptableObject
    {
        public BaseGameEvent[] gameEvents;
    }
}
