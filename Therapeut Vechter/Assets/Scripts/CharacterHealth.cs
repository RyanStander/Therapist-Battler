using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] protected float maxHealth;
    protected float CurrentHealth;

    protected virtual void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= CurrentHealth;
    }

    public void RegainHealth(float heal)
    {
        CurrentHealth += heal;
    }
}
