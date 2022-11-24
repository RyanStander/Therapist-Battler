using FMODUnity;
using UnityEngine;
using UnityEngine.VFX;

namespace Effects
{
    public class TakeDamageOnEffectEnd : MonoBehaviour
    {
        private EffectData effectData;
        private float damageToTake;
        private EventReference enemyHurtSound;

        private float timeStamp;

        private void Start()
        {
            effectData = transform.GetComponent<EffectData>();
            timeStamp = Time.time + effectData.EffectDuration;
        }

        private void Update()
        {
            if (timeStamp >= Time.time) return;
            EventManager.currentManager.AddEvent(new DamageEnemy(damageToTake));
            EventManager.currentManager.AddEvent(new DamageEnemyVisuals(damageToTake));
            EventManager.currentManager.AddEvent(new UpdateTotalScore(damageToTake));
            EventManager.currentManager.AddEvent(new PlaySfxAudio(enemyHurtSound));
            Destroy(gameObject);
        }

        public void SetEffectData(float damage, EventReference eventReference)
        {
            damageToTake = damage;
            enemyHurtSound = eventReference;
        }
    }
}