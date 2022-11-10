using UnityEngine;
using UnityEngine.Audio;

namespace MainMenu
{
    public class ChangeAudio : MonoBehaviour
    {
        [SerializeField] private AudioMixer[] audioMixer;
        [SerializeField] private string[] volumeName;
        [SerializeField] private GameObject menuObjectZero;
        [SerializeField] private GameObject menuObjectOne;
        [SerializeField] private GameObject menuObjectTwo;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && menuObjectOne.activeInHierarchy == false)
            {
                menuObjectZero.SetActive(true);
                menuObjectTwo.SetActive(false);
            }
        }

        public void SetVolume(float sliderValue)
        {
            audioMixer[0].SetFloat(volumeName[0], Mathf.Log10(sliderValue) * 20);
        }

        public void VolumeMusic(float sliderValue)
        {
            audioMixer[1].SetFloat(volumeName[1], Mathf.Log10(sliderValue) * 20);
        }

        public void VolumeAmbience(float sliderValue)
        {
            audioMixer[2].SetFloat(volumeName[2], Mathf.Log10(sliderValue) * 20);
        }

        public void VolumeDialog(float sliderValue)
        {
            audioMixer[3].SetFloat(volumeName[3], Mathf.Log10(sliderValue) * 20);
        }

        public void volumeSfx(float sliderValue)
        {
            audioMixer[4].SetFloat(volumeName[4], Mathf.Log10(sliderValue) * 20);
        }
    }
}