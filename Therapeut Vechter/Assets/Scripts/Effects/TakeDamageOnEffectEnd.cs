using FMODUnity;
using UnityEngine;
using UnityEngine.VFX;

namespace Effects
{
    public class TakeDamageOnEffectEnd : MonoBehaviour
    {
        private DamageEffectData effectData;
        private float damageToTake;
        private EventReference hitSfx;
        private bool isNormalAttack;

        private float effectLifetimeTimeStamp;
        private float timeUntilDamage;
        private bool hadDoneDamage;

        private bool isPlayerAttack;

        private void Start()
        {
            effectData = transform.GetComponent<DamageEffectData>();
            effectLifetimeTimeStamp = Time.time + effectData.EffectDuration;
            timeUntilDamage = Time.time + effectData.TimeUntilDamage;
        }

        private void Update()
        {
            if (timeUntilDamage <= Time.time && !hadDoneDamage)
            {
                if (isPlayerAttack)
                {
                    EventManager.currentManager.AddEvent(new DamageEnemy(damageToTake));
                    EventManager.currentManager.AddEvent(new DamageEnemyVisuals(damageToTake));
                    EventManager.currentManager.AddEvent(new UpdateTotalScore(damageToTake));
                }
                else
                {
                    EventManager.currentManager.AddEvent(new DamagePlayer(damageToTake));
                }

                EventManager.currentManager.AddEvent(new PlaySfxAudio(hitSfx));
                if (!isNormalAttack)
                    EventManager.currentManager.AddEvent(new UpdateComboScore(false, 0, 0));
                hadDoneDamage = true;
            }


            if (effectLifetimeTimeStamp >= Time.time || !hadDoneDamage) return;
            Destroy(gameObject);
        }

        public void SetEffectData(float damage, EventReference eventReference, bool effectIsNormalAttack = true,
            bool isPlayerAttack = true)
        {
            damageToTake = damage;
            hitSfx = eventReference;
            isNormalAttack = effectIsNormalAttack;
            this.isPlayerAttack = isPlayerAttack;
        }
    }
}