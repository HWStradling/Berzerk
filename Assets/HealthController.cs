using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour, IAttackable
{
    [SerializeField] public float CurrentHealth;
    [SerializeField] public float MaxHealth;
    private float gradualHealAmount = 0f;
    private bool allowGradualHeal;

    [SerializeField] UnityEvent<float> OnDamaged;

    public void ApplyDamage(float damage)
    {
        if (CurrentHealth - damage <= 0)
        {
            allowGradualHeal = false;
            CurrentHealth = 0;
            OnDamaged?.Invoke(CurrentHealth / MaxHealth);
            Die();
        }
        else
        {
            CurrentHealth -= damage;
            OnDamaged?.Invoke(CurrentHealth / MaxHealth);
            // play damage animation
        };
    }

    public void Die()
    {
        // play death animation,
        // load death scene;
        Destroy(gameObject); ;
    }

    public void GradualHeal()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //GradualHeal();
    }

    public void ApplyHeal(float healAmount)
    {
        throw new System.NotImplementedException();
    }

    public void SetMaxHealth(float newMaxHealth)
    {
        MaxHealth = newMaxHealth;
    }
}
