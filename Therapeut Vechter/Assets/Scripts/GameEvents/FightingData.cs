using Exercises;
using FMODUnity;
using UnityEngine;

namespace GameEvents
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Game Events/Fight Event")]
    public class FightingData : BaseGameEvent
    {
        [Header("Enemy Stats")] public GameObject enemyGameObject;
        public float enemyHealth = 100;
        public float enemyDamage = 10;
        public GameObject enemyDamageEffect;
        public EventReference enemyAttackSound;
        public EventReference enemyHurtSound;

        [Tooltip("This is the order in which the player attacks with, if it reaches the end it goes back to the start")]
        public PlayerAttackSequence[] playerAttackSequence; //Using jagged arrays as they are not size restricted

        [Tooltip("If true it will advance to the next audio stage of the music at the start of this.")]
        public bool advanceToNextAudioStageAtStartOfExerciseSet = false;

        [Tooltip("If true it will advance to the next audio stage of the music at the end of this.")]
        public bool advanceToNextAudioStageAtEndOfExerciseSet = false;
    }
}