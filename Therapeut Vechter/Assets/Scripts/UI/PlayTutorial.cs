using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTutorial : MonoBehaviour
{
    private EventInstance dialogueAudioEventInstance;
    [SerializeField]
    private Texture[] image;
    public FMODUnity.EventReference[] SoundToPlay;
    public Button NextButton;
    public RawImage tutorialRotater;

    private int i;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowNextImage()
    {
        
        if (i > SoundToPlay.Length) 
        {
            Debug.Log("End reached");
            NextButton.enabled = false;
        }

        dialogueAudioEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

        RuntimeManager.StudioSystem.getEvent(SoundToPlay[i].Path, out var eventDescription);
        if (!eventDescription.isValid())
            return;

        dialogueAudioEventInstance = RuntimeManager.CreateInstance(SoundToPlay[i].Path);
        tutorialRotater.texture = image[i];

        dialogueAudioEventInstance.start();

    }

    public void IncreaseValue()
    {
        i++;
        if (i >= SoundToPlay.Length)
        {
            NextButton.enabled = false;
            return;
        }
    }
}
