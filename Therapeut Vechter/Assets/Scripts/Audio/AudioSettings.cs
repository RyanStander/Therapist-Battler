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

        private float masterVolume = 1f;
        private float musicVolume, sfxVolume, voVolume, ambienceVolume = 0.5f;

        private void Awake()
        {
            master = RuntimeManager.GetBus(busPath + masterPath);
            music = RuntimeManager.GetBus(busPath + musicPath);
            sfx = RuntimeManager.GetBus(busPath + sfxPath);
            vo = RuntimeManager.GetBus(busPath + voPath);
            ambience = RuntimeManager.GetBus(busPath + ambiencePath);
        }

        private void FixedUpdate()
        {
            master.setVolume(masterVolume);
            music.setVolume(musicVolume);
            sfx.setVolume(sfxVolume);
            vo.setVolume(voVolume);
            ambience.setVolume(ambienceVolume);
        }

        public void SetMasterVolume(float newVolume)
        {
            masterVolume = newVolume;
        }

        public void SetMusicVolume(float newVolume)
        {
            musicVolume = newVolume;
        }

        public void SetSfxVolume(float newVolume)
        {
            sfxVolume = newVolume;
        }

        public void SetVoPath(float newVolume)
        {
            voVolume = newVolume;
        }

        public void SetAmbiencePath(float newVolume)
        {
            ambienceVolume = newVolume;
        }
    }
}