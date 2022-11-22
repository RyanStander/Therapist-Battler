using FMODUnity;
using UnityEngine;

namespace GameEvents
{
    public class BaseGameEvent : ScriptableObject
    {
        [Header("Override")]
        public bool OverrideCurrentlyPlayingMusic;
        public EventReference OverrideMusic;
        public Sprite BackgroundSprite;
    }
}
