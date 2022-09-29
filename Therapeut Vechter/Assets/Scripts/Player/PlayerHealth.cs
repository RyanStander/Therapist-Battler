using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerHealth : CharacterHealth
    {
        [SerializeField] private SliderBar healthBar;

        protected override void Awake()
        {
            base.Awake();
            healthBar.SetValues(CurrentHealth,maxHealth);
        }
    }
}
