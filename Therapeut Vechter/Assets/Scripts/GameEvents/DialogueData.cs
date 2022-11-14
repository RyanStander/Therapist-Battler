using UnityEngine;

namespace GameEvents
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Game Events/Dialogue Event")]
    public class DialogueData : BaseGameEvent
    {
        [Header("Dialogue Specific")]
        public AudioClip DialogueClip;
    }
}
