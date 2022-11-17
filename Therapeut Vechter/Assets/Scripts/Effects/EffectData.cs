using UnityEngine;

namespace Effects
{
    /// <summary>
    /// This is needed because Visual Effects do not have an easy link to whether they are playing, could potentially hold more data 
    /// </summary>
    public class EffectData : MonoBehaviour
    {
        [Tooltip("How long the effect will last")]
        public float EffectDuration;
    }
}