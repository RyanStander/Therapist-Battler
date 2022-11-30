using System;
using FMODUnity;
using UnityEngine;

namespace Audio
{
    public class AudioSettings : MonoBehaviour
    {
        [SerializeField] private string masterPath = "Master";
        [SerializeField] private string musicPath = "Master/Music";
        [SerializeField] private string sfxPath = "Master/SFX";
        [SerializeField] private string voPath = "Master/VO";
        [SerializeField] private string ambiencePath = "Master/Ambient";

        private string busPath = "bus:/";

        private FMOD.Studio.Bus master;
        private FMOD.Studio.Bus music;
        private FMOD.Studio.Bus sfx;
        private FMOD.Studio.Bus vo;
        private FMOD.Studio.Bus ambience;

        private void Awake()
        {
            master = RuntimeManager.GetBus(busPath + masterPath);
            music = RuntimeManager.GetBus(busPath + musicPath);
            sfx = RuntimeManager.GetBus(busPath + sfxPath);
            vo = RuntimeManager.GetBus(busPath + voPath);
            ambience = RuntimeManager.GetBus(busPath + ambiencePath);
        }

        public void SetMasterVolume(float newVolume)
        {
            master.setVolume(newVolume);
        }

        public void SetMusicVolume(float newVolume)
        {
            music.setVolume(newVolume);
        }

        public void SetSfxVolume(float newVolume)
        {
            sfx.setVolume(newVolume);
        }

        public void SetVoPath(float newVolume)
        {
            vo.setVolume(newVolume);
        }

        public void SetAmbiencePath(float newVolume)
        {
            ambience.setVolume(newVolume);
        }
    }
}