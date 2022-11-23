using UnityEngine;
using UnityEngine.VFX;

namespace Effects
{
    public class TakeDamageOnEffectEnd : MonoBehaviour
    {
        private EffectData effectData;
        private float damageToTake;

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
            Destroy(gameObject);
        }

        public void SetEffectDamage(float damage)
        {
            damageToTake = damage;
        }
    }
}