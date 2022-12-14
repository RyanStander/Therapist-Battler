using UnityEngine;

namespace Effects
{
    public class EffectSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject playerNormalAttackEffect;
        [SerializeField] private GameObject playerComboAttackEffect;
        
        private void OnEnable()
        {
            EventManager.currentManager.Subscribe(EventType.CreateNormalAttack,OnNormalAttack);
            EventManager.currentManager.Subscribe(EventType.CreateComboAttack,OnComboAttack);
        }

        private void OnDisable()
        {
            EventManager.currentManager.Unsubscribe(EventType.CreateNormalAttack,OnNormalAttack);
            EventManager.currentManager.Unsubscribe(EventType.CreateComboAttack,OnComboAttack);
        }

        private void OnNormalAttack(EventData eventData)
        {
            if (eventData is CreateNormalAttack createNormalAttack)
            {
                if (!createNormalAttack.IsPlayerAttack&& createNormalAttack.AttackEffect==null)
                {
                    Debug.LogError("If an attack is not from the player, it must have an effect specified");
                    return;
                }
                
                var normalAttack = Instantiate(createNormalAttack.IsPlayerAttack ? playerNormalAttackEffect : createNormalAttack.AttackEffect, transform);

                var damageEffect = normalAttack.AddComponent<TakeDamageOnEffectEnd>();
                damageEffect.SetEffectData(createNormalAttack.Damage,createNormalAttack.OnHitSfx,true,createNormalAttack.IsPlayerAttack);
            }
            else
            {
                Debug.Log("Received event type CreatePlayerNormalAttack but EventData was not of type CreatePlayerNormalAttack");
            }

        }

        private void OnComboAttack(EventData eventData)
        {
            if (eventData is CreateComboAttack createComboAttack)
            {
                var comboAttack = Instantiate(playerComboAttackEffect, transform);
                var damageEffect = comboAttack.AddComponent<TakeDamageOnEffectEnd>();
                damageEffect.SetEffectData(createComboAttack.Damage,createComboAttack.OnHitSfx,false);
            }
            else
            {
                Debug.Log("Received event type CreatePlayerComboAttack but EventData was not of type CreatePlayerComboAttack");
            }
        }
    }
}