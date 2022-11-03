using UnityEngine;

namespace Enemy
{
    /// <summary>
    /// Simple script to manage enemy health bars
    /// </summary>
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
