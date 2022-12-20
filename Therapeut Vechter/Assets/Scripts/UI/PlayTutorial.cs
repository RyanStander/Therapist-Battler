using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTutorial : MonoBehaviour
{
    public FMODUnity.EventReference[] SoundToPlay;
    [SerializeField]
    private FMODUnity.StudioEventEmitter emitter;
    public Button NextButton;
    private bool ClickButton = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!emitter.IsPlaying() && ClickButton) 
        {
            foreach (var element in SoundToPlay)
            {
                FMODUnity.RuntimeManager.PlayOneShot(element);
                ClickButton = false;
            }
        }

        

    }

    private void Awake()
    {
        // adding a delegate with parameters
        NextButton.onClick.AddListener(delegate { ParameterOnClick("Button was pressed!"); });
    }

    private void ParameterOnClick(string test)
    {
        Debug.Log(test);
        ClickButton = true;
    }

    bool IsPlaying(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        instance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }
}
