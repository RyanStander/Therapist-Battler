using UnityEngine;

namespace Effects
{
    public class EffectSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject playerNormalAttackEffect;

        private void OnEnable()
        {
            EventManager.currentManager.Subscribe(EventType.CreatePlayerNormalAttack,OnPlayerNormalAttack);
        }

        private void OnDisable()
        {
            EventManager.currentManager.Unsubscribe(EventType.CreatePlayerNormalAttack,OnPlayerNormalAttack);
        }

        private void OnPlayerNormalAttack(EventData eventData)
        {
            if (eventData is CreatePlayerNormalAttack createPlayerNormalAttack)
            {
                var normalAttack = Instantiate(playerNormalAttackEffect, transform);
                var damageEffect = normalAttack.AddComponent<TakeDamageOnEffectEnd>();
                damageEffect.SetEffectDamage(createPlayerNormalAttack.Damage);
            }
            else
            {
                Debug.Log("Received event type CreatePlayerNormalAttack but EventData was not of type CreatePlayerNormalAttack");
            }

        }
    }
}