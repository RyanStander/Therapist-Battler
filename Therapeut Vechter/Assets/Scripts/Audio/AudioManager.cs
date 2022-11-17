using System;
using FMOD.Studio;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        private EventInstance dialogueAudioEventInstance;


        [SerializeField]private bool isPlayingDialogueAudio=true;

        #region Runtime

        private void OnEnable()
        {
            EventManager.currentManager.Subscribe(EventType.PlayDialogueAudio, OnPlayDialogueAudio);
            EventManager.currentManager.Subscribe(EventType.PlaySfxAudio, OnPlaySfxAudio);
            EventManager.currentManager.Subscribe(EventType.PlayMusicAudio, OnPlayMusicAudio);
        }

        private void OnDisable()
        {
            EventManager.currentManager.Unsubscribe(EventType.PlayDialogueAudio, OnPlayDialogueAudio);
            EventManager.currentManager.Unsubscribe(EventType.PlaySfxAudio, OnPlaySfxAudio);
            EventManager.currentManager.Unsubscribe(EventType.PlayMusicAudio, OnPlayMusicAudio);
        }

        private void FixedUpdate()
        {
            if(!isPlayingDialogueAudio)
                return;

            dialogueAudioEventInstance.getPlaybackState(out var state);
            if (state != PLAYBACK_STATE.STOPPED) return;
            
            Debug.Log("Stopped");
            isPlayingDialogueAudio = false;
            EventManager.currentManager.AddEvent(new DialogueAudioStatusUpdate(isPlayingDialogueAudio));
            //dialogueAudioEventInstance.release();
        }

        #endregion

        #region MyRegion

        private void OnPlayDialogueAudio(EventData eventData)
        {
            if (eventData is PlayDialogueAudio playDialogueAudio)
            {
                Debug.Log("Hello");
                //Send event to state that the dialogueAudio is in use
                dialogueAudioEventInstance = FMODUnity.RuntimeManager.CreateInstance(playDialogueAudio.EventSoundPath);
                dialogueAudioEventInstance.start();
                isPlayingDialogueAudio = true;
                EventManager.currentManager.AddEvent(new DialogueAudioStatusUpdate(isPlayingDialogueAudio));
            }
        }

        private void OnPlaySfxAudio(EventData eventData)
        {
        }

        private void OnPlayMusicAudio(EventData eventData)
        {
        }

        #endregion
    }
}