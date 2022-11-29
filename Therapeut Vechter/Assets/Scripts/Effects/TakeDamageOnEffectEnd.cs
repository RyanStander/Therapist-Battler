using FMODUnity;
using UnityEngine;
using UnityEngine.VFX;

namespace Effects
{
    public class TakeDamageOnEffectEnd : MonoBehaviour
    {
        private DamageEffectData effectData;
        private float damageToTake;
        private EventReference enemyHurtSound;
        private bool isNormalAttack;
        
        private float effectLifetimeTimeStamp;
        private float timeUntilDamage;
        private bool hadDoneDamage;

        private void Start()
        {
            effectData = transform.GetComponent<DamageEffectData>();
            effectLifetimeTimeStamp = Time.time + effectData.EffectDuration;
            timeUntilDamage = Time.time + effectData.TimeUntilDamage;
        }

        private void Update()
        {
            if (timeUntilDamage<=Time.time && !hadDoneDamage)
            {
                Debug.Log("Damage at : "+ timeUntilDamage + " | "+Time.time);
                EventManager.currentManager.AddEvent(new DamageEnemy(damageToTake));
                EventManager.currentManager.AddEvent(new DamageEnemyVisuals(damageToTake));
                EventManager.currentManager.AddEvent(new UpdateTotalScore(damageToTake));
                EventManager.currentManager.AddEvent(new PlaySfxAudio(enemyHurtSound));
                if (!isNormalAttack)
                    EventManager.currentManager.AddEvent(new UpdateComboScore(false, 0, 0));
                hadDoneDamage = true;
            }
            
            
            if (effectLifetimeTimeStamp >= Time.time) return;
            Debug.Log("destroy at : "+ effectLifetimeTimeStamp + " | "+Time.time);
            Destroy(gameObject);
        }

        public void SetEffectData(float damage, EventReference eventReference, bool effectIsNormalAttack=true)
        {
            damageToTake = damage;
            enemyHurtSound = eventReference;
            isNormalAttack = effectIsNormalAttack;
        }
    }
}