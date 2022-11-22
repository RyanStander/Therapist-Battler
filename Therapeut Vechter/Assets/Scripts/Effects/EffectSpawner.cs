using UnityEngine;

namespace Effects
{
    public class EffectSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject playerNormalAttackEffect;
        [SerializeField] private GameObject playerComboAttackEffect;
        
        private void OnEnable()
        {
            EventManager.currentManager.Subscribe(EventType.CreatePlayerNormalAttack,OnPlayerNormalAttack);
            EventManager.currentManager.Subscribe(EventType.CreatePlayerComboAttack,OnPlayerComboAttack);
        }

        private void OnDisable()
        {
            EventManager.currentManager.Unsubscribe(EventType.CreatePlayerNormalAttack,OnPlayerNormalAttack);
            EventManager.currentManager.Unsubscribe(EventType.CreatePlayerComboAttack,OnPlayerComboAttack);
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

        private void OnPlayerComboAttack(EventData eventData)
        {
            if (eventData is CreatePlayerComboAttack createPlayerComboAttack)
            {
                var comboAttack = Instantiate(playerComboAttackEffect, transform);
                var damageEffect = comboAttack.AddComponent<TakeDamageOnEffectEnd>();
                damageEffect.SetEffectDamage(createPlayerComboAttack.Damage);
            }
            else
            {
                Debug.Log("Received event type CreatePlayerComboAttack but EventData was not of type CreatePlayerComboAttack");
            }
        }
    }
}