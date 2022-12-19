using System;
using FMODUnity;
using UnityEngine;

namespace Audio
{
    public class Bus : MonoBehaviour
    {
        private FMOD.Studio.Bus bus;

        [TextArea][SerializeField] private string busPath="";
        [SerializeField] [Range(-80f, 10f)] private float busVolume;

        private void Start()
        {
            bus = RuntimeManager.GetBus("bus:/"+busPath);
        }

        private void Update()
        {
            bus.setVolume(DecibelToLinear(busVolume));
        }

        private float DecibelToLinear(float dB)
        {
            var linear = Mathf.Pow(10.0f, dB / 20f);

            return linear;
        }
    }
}
