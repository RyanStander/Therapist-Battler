using System;
using Events;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        private EventInstance dialogueAudioEventInstance;
        private EventInstance exerciseDialogueAudioEventInstance;
        private EventInstance musicAudioEventInstance;
        private EventInstance ambienceAudioEventInstance;
        private EventInstance sfxAudioEventInstance;


        private bool isPlayingDialogueAudio;
        private const string MusicParameterName = "MusicStage";

        #region Runtime

        private void OnEnable()
        {
            EventManager.currentManager.Subscribe(EventType.PlayDialogueAudio, OnPlayDialogueAudio);
            EventManager.currentManager.Subscribe(EventType.StopDialogue, OnStopDialogue);
            EventManager.currentManager.Subscribe(EventType.PlaySfxAudio, OnPlaySfxAudio);
            EventManager.currentManager.Subscribe(EventType.PlayMusicAudio, OnPlayMusicAudio);
            EventManager.currentManager.Subscribe(EventType.PlayExerciseDialogueAudio, OnPlayExerciseDialogueAudio);
            EventManager.currentManager.Subscribe(EventType.PlayAmbienceAudio, OnPlayAmbienceAudio);
            EventManager.currentManager.Subscribe(EventType.AdvanceMusicStage, OnAdvanceMusicStage);
        }

        private void OnDisable()
        {
            EventManager.currentManager.Unsubscribe(EventType.PlayDialogueAudio, OnPlayDialogueAudio);
            EventManager.currentManager.Unsubscribe(EventType.StopDialogue, OnStopDialogue);
            EventManager.currentManager.Unsubscribe(EventType.PlaySfxAudio, OnPlaySfxAudio);
            EventManager.currentManager.Unsubscribe(EventType.PlayMusicAudio, OnPlayMusicAudio);
            EventManager.currentManager.Unsubscribe(EventType.PlayExerciseDialogueAudio, OnPlayExerciseDialogueAudio);
            EventManager.currentManager.Unsubscribe(EventType.PlayAmbienceAudio, OnPlayAmbienceAudio);
            EventManager.currentManager.Unsubscribe(EventType.AdvanceMusicStage, OnAdvanceMusicStage);
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

        #region OnEvents

        private void OnPlayDialogueAudio(EventData eventData)
        {
            if (!eventData.IsEventOfType<PlayDialogueAudio>(out var playDialogueAudio))
                return;

            RuntimeManager.StudioSystem.getEvent(playDialogueAudio.EventSoundPath.Path, out var eventDescription);

            eventDescription.isStream(out var oneshot);
            eventDescription.getPath(out var path);
            Debug.Log(path);
            
            if (!eventDescription.isValid())
                return;

            dialogueAudioEventInstance = RuntimeManager.CreateInstance(playDialogueAudio.EventSoundPath);

            dialogueAudioEventInstance.start();
        }

        private void OnPlayExerciseDialogueAudio(EventData eventData)
        {
            if (!eventData.IsEventOfType<PlayExerciseDialogueAudio>(out var playExerciseDialogueAudio))
                return;

            RuntimeManager.StudioSystem.getEvent(playExerciseDialogueAudio.EventSoundPath.Path,
                out var eventDescription);

            if (!eventDescription.isValid())
                return;

            exerciseDialogueAudioEventInstance.stop(STOP_MODE.IMMEDIATE);

            exerciseDialogueAudioEventInstance = RuntimeManager.CreateInstance(playExerciseDialogueAudio.EventSoundPath);

            exerciseDialogueAudioEventInstance.start();
            isPlayingDialogueAudio = true;
            EventManager.currentManager.AddEvent(new DialogueAudioStatusUpdate(isPlayingDialogueAudio));
        }

        private void OnPlaySfxAudio(EventData eventData)
        {
            if (!eventData.IsEventOfType<PlaySfxAudio>(out var sfxAudio))
                return;

            RuntimeManager.StudioSystem.getEvent(sfxAudio.EventSoundPath.Path, out var eventDescription);

            if (!eventDescription.isValid())
                return;

            sfxAudioEventInstance = RuntimeManager.CreateInstance(sfxAudio.EventSoundPath);

            sfxAudioEventInstance.start();
            sfxAudioEventInstance.release();
        }

        private void OnPlayMusicAudio(EventData eventData)
        {
            if (eventData.IsEventOfType<PlayMusicAudio>(out var musicAudio))
                return;

            RuntimeManager.StudioSystem.getEvent(musicAudio.EventSoundPath.Path, out var eventDescription);
            if (!eventDescription.isValid())
                return;

            //musicAudioEventInstance.stop(STOP_MODE.IMMEDIATE);

            musicAudioEventInstance = RuntimeManager.CreateInstance(musicAudio.EventSoundPath);

            musicAudioEventInstance.start();
        }

        private void OnAdvanceMusicStage(EventData eventData)
        {
            if (eventData.IsEventOfType<AdvanceMusicStage>())
                return;

            musicAudioEventInstance.getParameterByName(MusicParameterName, out var musicParameterValue);

            musicParameterValue++;
            
            musicAudioEventInstance.setParameterByName(MusicParameterName, musicParameterValue);
        }

        private void OnPlayAmbienceAudio(EventData eventData)
        {
            if (eventData.IsEventOfType<PlayAmbienceAudio>(out var playAmbienceAudio))
                return;

            RuntimeManager.StudioSystem.getEvent(playAmbienceAudio.EventSoundPath.Path, out var eventDescription);
            if (!eventDescription.isValid())
                return;

            //ambienceAudioEventInstance.stop(STOP_MODE.IMMEDIATE);

            ambienceAudioEventInstance = RuntimeManager.CreateInstance(playAmbienceAudio.EventSoundPath);

            ambienceAudioEventInstance.start();
        }

        private void OnStopDialogue(EventData eventData)
        {
            if (eventData.IsEventOfType<StopDialogue>())
                return;

            dialogueAudioEventInstance.stop(STOP_MODE.IMMEDIATE);
        }

        #endregion
    }
}