using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenuScript : MonoBehaviour
{
    public AudioMixer Mixer;
    public GameObject[] Menus;
    public string ChangeScene;
    public string[] VolumeName;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&& Menus[1].activeInHierarchy == false)
        {
                Menus[0].SetActive(true);
                Menus[2].SetActive(false);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(ChangeScene);
    }
    public void QuitGame()
    {
        Debug.Log("Quit Application");
        Application.Quit();
    }

    public void SetLevel(float sliderValue)
    {
        Mixer.SetFloat(VolumeName[0], Mathf.Log10(sliderValue) * 20);
    }
}
