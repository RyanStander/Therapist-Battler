using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ChangeAudio : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private GameObject[] menus;
    [SerializeField] private string[] volumeName;
    private GameObject menuObjectZero;
    private GameObject menuObjectOne;
    private GameObject menuObjectTwo;

    void Start()
    {
        menuObjectZero = menus[0];
        menuObjectOne = menus[1];
        menuObjectTwo = menus[2];
    }
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
