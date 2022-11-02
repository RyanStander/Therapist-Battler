using UnityEngine;

namespace GameEvents
{
    public class BaseGameEvent : ScriptableObject
    {
        [Header("Override")]
        public bool OverrideCurrentlyPlayingMusic;
        public AudioClip OverrideMusic;
        public Sprite BackgroundSprite;
    }
}
