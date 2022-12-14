using FMODUnity;
using UnityEngine;

namespace GameEvents
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Game Events/Dialogue Event")]
    public class DialogueData : BaseGameEvent
    {
        [Header("Dialogue Specific")]
        [Tooltip("The path is based in the events path of fmod")]
        public EventReference EventPath;
    }
}
