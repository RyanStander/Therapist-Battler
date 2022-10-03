using UnityEngine;

namespace Enemy
{
    public class EnemyHealth : CharacterHealth
    {
        [SerializeField] private SliderBar healthBar;
        
        protected override void Awake()
        {
            base.Awake();
            healthBar.SetValues(CurrentHealth,maxHealth);
        }
    }
}
