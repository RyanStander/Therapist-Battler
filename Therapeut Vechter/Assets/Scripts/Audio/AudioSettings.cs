using System;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    public class AudioSettings : MonoBehaviour
    {
        [SerializeField] private string masterPath = "Master";
        [SerializeField] private Slider masterSlider;
        
        [SerializeField] private string musicPath = "Master/Music";
        [SerializeField] private Slider musicSlider;
        
        [SerializeField] private string sfxPath = "Master/SFX";
        [SerializeField] private Slider sfxSlider;
        
        [SerializeField] private string voPath = "Master/VO";
        [SerializeField] private Slider voSlider;
        
        [SerializeField] private string ambiencePath = "Master/Ambient";
        [SerializeField] private Slider ambienceSlider;

        private string busPath = "bus:/";

        private FMOD.Studio.Bus master;
        private FMOD.Studio.Bus music;
        private FMOD.Studio.Bus sfx;
        private FMOD.Studio.Bus vo;
        private FMOD.Studio.Bus ambience;

        private void Awake()
        {
            master = RuntimeManager.GetBus(busPath + masterPath);
            var masterVolumeLevel = PlayerPrefs.GetFloat("MasterVolume", 1);
            master.setVolume(masterVolumeLevel);
            masterSlider.value = masterVolumeLevel;
            
            music = RuntimeManager.GetBus(busPath + musicPath);
            var musicVolumeLevel=PlayerPrefs.GetFloat("musicVolume", 1);
            music.setVolume(musicVolumeLevel);
            musicSlider.value = musicVolumeLevel;
            
            sfx = RuntimeManager.GetBus(busPath + sfxPath);
            var sfxVolumeLevel = PlayerPrefs.GetFloat("SfxVolume", 1);
            sfx.setVolume(sfxVolumeLevel);
            sfxSlider.value = sfxVolumeLevel;
            
            vo = RuntimeManager.GetBus(busPath + voPath);
            var voVolumeLevel = PlayerPrefs.GetFloat("VoVolume", 1);
            vo.setVolume(voVolumeLevel);
            voSlider.value = voVolumeLevel;
            
            ambience = RuntimeManager.GetBus(busPath + ambiencePath);
            var ambienceVolumeLevel = PlayerPrefs.GetFloat("AmbienceVolume", 1);
            ambience.setVolume(ambienceVolumeLevel);
            ambienceSlider.value = ambienceVolumeLevel;
        }

        public void SetMasterVolume(float newVolume)
        {
            PlayerPrefs.SetFloat("MasterVolume",newVolume);
            master.setVolume(newVolume);
        }

        public void SetMusicVolume(float newVolume)
        {
            PlayerPrefs.SetFloat("MusicVolume",newVolume);
            music.setVolume(newVolume);
        }

        public void SetSfxVolume(float newVolume)
        {
            PlayerPrefs.SetFloat("SfxVolume",newVolume);
            sfx.setVolume(newVolume);
        }

        public void SetVoPath(float newVolume)
        {
            PlayerPrefs.SetFloat("VoVolume",newVolume);
            vo.setVolume(newVolume);
        }

        public void SetAmbiencePath(float newVolume)
        {
            PlayerPrefs.SetFloat("AmbienceVolume",newVolume);
            ambience.setVolume(newVolume);
        }
    }
}