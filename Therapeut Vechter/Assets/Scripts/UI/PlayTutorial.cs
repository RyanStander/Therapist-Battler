using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class PlayTutorial : MonoBehaviour
    {
        [SerializeField] private Texture[] tutorialImage;
        [SerializeField] private EventReference[] soundToPlay;
        [SerializeField] private Button nextButton;
        [SerializeField] private RawImage tutorialRotater;
        [SerializeField] private string sceneName;
        [SerializeField] private GameObject outroText;

        private int index;
        private EventInstance dialogueAudioEventInstance;

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

            RuntimeManager.StudioSystem.getEvent(soundToPlay[index].Path, out var eventDescription);
            if (!eventDescription.isValid())
                return;

            dialogueAudioEventInstance = RuntimeManager.CreateInstance(soundToPlay[index].Path);
            tutorialRotater.texture = tutorialImage[index];

            dialogueAudioEventInstance.start();
        }

        public void IncreaseValue()
        {
            index++;

            if (index < soundToPlay.Length)
                return;

            nextButton.enabled = false;
            outroText.SetActive(true);
        }
    }
}