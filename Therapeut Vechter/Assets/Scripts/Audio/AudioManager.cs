using System;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource dialogueAudioSource;
        [SerializeField] private AudioSource sfxAudioSource;
        [SerializeField] private AudioSource musicAudioSource;

        private bool isPlayDialogueAudio = false;

        #region Runtime

        private void OnEnable()
        {
            EventManager.currentManager.Subscribe(EventType.PlayDialogueAudio,OnPlayDialogueAudio);
            EventManager.currentManager.Subscribe(EventType.PlaySfxAudio,OnPlaySfxAudio);
            EventManager.currentManager.Subscribe(EventType.PlayMusicAudio,OnPlayMusicAudio);
        }

        private void OnDisable()
        {
            EventManager.currentManager.Unsubscribe(EventType.PlayDialogueAudio,OnPlayDialogueAudio);
            EventManager.currentManager.Unsubscribe(EventType.PlaySfxAudio,OnPlaySfxAudio);
            EventManager.currentManager.Unsubscribe(EventType.PlayMusicAudio,OnPlayMusicAudio);
        }

        private void FixedUpdate()
        {
            if (!isPlayDialogueAudio)
                return;
            if (dialogueAudioSource.isPlaying)
                return;
            
            
        }

        #endregion

        #region MyRegion

        private void OnPlayDialogueAudio(EventData eventData)
        {
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