using FMODUnity;
using UnityEngine;

namespace Audio
{
    public class VCA : MonoBehaviour
    {
        private FMOD.Studio.VCA vca;

        [SerializeField] [Range(-80f, 10f)] private float vcaVolume;

        private void Start()
        {
            vca = RuntimeManager.GetVCA("vca/Music:");
        }

        private void Update()
        {
            vca.setVolume(DecibelToLinear(vcaVolume));
        }

        private float DecibelToLinear(float dB)
        {
            var linear = Mathf.Pow(10.0f, dB / 20f);

            return linear;
        }
    }
}