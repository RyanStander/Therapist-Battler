using Exercises;
using UnityEngine;

namespace GameEvents
{
    [CreateAssetMenu(menuName = "Game Events/Fight Event")]
    public class FightingData : BaseGameEvent
    {
        public Sprite backgroundSprite;
        public Sprite enemySprite;
        public float enemyHealth=100;
        public float enemyDamage=10;
        public AudioClip[] enemyAttackSounds;
        public AudioClip[] enemyAttackedSounds;
        [Tooltip("This is the order in which the player attacks with, if it reaches the end it goes back to the start")]
        public PlayerAttackSequence[] playerAttackSequence;//Using jagged arrays as they are not size restricted
    }

    [System.Serializable]
    public class PlayerAttackSequence
    {
        public AudioClip exerciseName;
        public PoseDataSet[] playerAttack;
    }
}
