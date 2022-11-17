using System;
using FMOD.Studio;
using FMODUnity;
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
            if (!isPlayingDialogueAudio)
                return;

            dialogueAudioEventInstance.getPlaybackState(out var state);
            if (state != PLAYBACK_STATE.STOPPING) return;

            isPlayingDialogueAudio = false;
            EventManager.currentManager.AddEvent(new DialogueAudioStatusUpdate(isPlayingDialogueAudio));
            dialogueAudioEventInstance.release();
        }

        #endregion

        #region MyRegion

        private void OnPlayDialogueAudio(EventData eventData)
        {
            if (eventData is PlayDialogueAudio playDialogueAudio)
            {
                RuntimeManager.StudioSystem.getEvent(playDialogueAudio.EventSoundPath.Path, out var eventDescription);
                if (!eventDescription.isValid())
                    return;
                
                dialogueAudioEventInstance = RuntimeManager.CreateInstance(playDialogueAudio.EventSoundPath);

                dialogueAudioEventInstance.start();
                isPlayingDialogueAudio = true;
                EventManager.currentManager.AddEvent(new DialogueAudioStatusUpdate(isPlayingDialogueAudio));
            }
        }

        private void OnPlaySfxAudio(EventData eventData)
        {
            if (eventData is PlaySfxAudio sfxAudio)
            {
                RuntimeManager.StudioSystem.getEvent(sfxAudio.EventSoundPath.Path, out var eventDescription);
                if (!eventDescription.isValid())
                    return;
                
                sfxAudioEventInstance = RuntimeManager.CreateInstance(sfxAudio.EventSoundPath);
                
                sfxAudioEventInstance.start();
                sfxAudioEventInstance.release();
            }
        }

        private void OnPlayMusicAudio(EventData eventData)
        {
            if (eventData is PlayMusicAudio musicAudio)
            {
                RuntimeManager.StudioSystem.getEvent(musicAudio.EventSoundPath.Path, out var eventDescription);
                if (!eventDescription.isValid())
                    return;
                
                musicAudioEventInstance = RuntimeManager.CreateInstance(musicAudio.EventSoundPath);

                musicAudioEventInstance.start();
                musicAudioEventInstance.release();
            }
        }

        #endregion
    }
}