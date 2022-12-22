using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayTutorial : MonoBehaviour
{
    private EventInstance dialogueAudioEventInstance;
    [SerializeField]
    private Texture[] tutorialImage;
    [SerializeField]
    private EventReference[] SoundToPlay;
    [SerializeField]
    private Button NextButton;
    [SerializeField]
    private RawImage tutorialRotater;
    [SerializeField] 
    private string sceneName;
    [SerializeField]
    private GameObject outroText;

    private int Index;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void ShowNextImage()
    {
        dialogueAudioEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

        RuntimeManager.StudioSystem.getEvent(SoundToPlay[Index].Path, out var eventDescription);
        if (!eventDescription.isValid())
            return;

        dialogueAudioEventInstance = RuntimeManager.CreateInstance(SoundToPlay[Index].Path);
        tutorialRotater.texture = tutorialImage[Index];

        dialogueAudioEventInstance.start();

    }

    public void IncreaseValue()
    {
        Index++;
        if (Index >= SoundToPlay.Length)
        {
            NextButton.enabled = false;
            outroText.SetActive(true);
            return;
        }
    }
}
