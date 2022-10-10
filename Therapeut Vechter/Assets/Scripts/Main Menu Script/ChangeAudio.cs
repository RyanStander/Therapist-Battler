using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ChangeAudio : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string[] volumeName;
    [SerializeField] private GameObject menuObjectZero;
    [SerializeField] private GameObject menuObjectOne;
    [SerializeField] private GameObject menuObjectTwo;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&& menuObjectOne.activeInHierarchy == false)
        {
            menuObjectZero.SetActive(true);
            menuObjectTwo.SetActive(false);
        }
    }
    public void SetVolume(float sliderValue)
    {
        audioMixer.SetFloat(volumeName[0], Mathf.Log10(sliderValue) * 20);
    }
}
