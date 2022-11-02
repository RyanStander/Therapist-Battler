using UnityEngine;

namespace GameEvents
{
    [CreateAssetMenu(menuName = "Game Events/Dialogue Event")]
    public class DialogueData : BaseGameEvent
    {
        [Header("Dialogue Specific")]
        public AudioClip DialogueClip;
    }
}
