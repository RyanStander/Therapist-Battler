using System;
using FMOD.Studio;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        private EventInstance dialogueAudioEventInstance;
        private EventInstance musicAudioEventInstance;
        private EventInstance sfxAudioEventInstance;


        private bool isPlayingDialogueAudio;

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
                dialogueAudioEventInstance = FMODUnity.RuntimeManager.CreateInstance(playDialogueAudio.EventSoundPath);
                dialogueAudioEventInstance.start();
                isPlayingDialogueAudio = true;
                EventManager.currentManager.AddEvent(new DialogueAudioStatusUpdate(isPlayingDialogueAudio));
            }

            //Send event to state that the dialogueAudio is in use
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