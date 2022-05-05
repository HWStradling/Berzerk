using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour, IAttackable
{
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private int maxLives;
    private int remainingLives;
    private float gradualHealAmount = 0f;
    private bool allowGradualHeal;



    [SerializeField] UnityEvent<float> OnHealthChanged;
    [SerializeField] UnityEvent<int> OnLivesChanged;
    [SerializeField] UnityEvent OnDeathRespawn;
    [SerializeField] UnityEvent OnDeathFinal;
    [SerializeField] UnityEvent<int> OnEnemyDeath;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        remainingLives = maxLives - 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
            //GradualHeal();
    }

    public void ApplyDamage(float damage)
    {
        if (currentHealth - damage <= 0)
        {
            allowGradualHeal = false;
            currentHealth = 0;
            OnHealthChanged?.Invoke(currentHealth / maxHealth);
            Die();
        }
        else
        {
            allowGradualHeal = true;
            currentHealth -= damage;
            OnHealthChanged?.Invoke(currentHealth / maxHealth);
            // play damage animation
        };
    }

    public void Die()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            OnDeathFinal?.Invoke();
            OnEnemyDeath?.Invoke(2);
            gameObject.SetActive(false);
        }
        else if (gameObject.CompareTag("Player"))
        {
            switch (remainingLives-1)
            {
                case 0:
                    OnDeathFinal?.Invoke();
                    break;
                default:
                    Respawn();
                    break;
            }
        }
        // play death animation,
        // load death scene;
        
    }
    public void Respawn()
    {
        ApplyHealing(maxHealth);
        SetRemainingLives(remainingLives - 1);
        OnDeathRespawn?.Invoke();
    }
    public void Spawn()
    {
        ApplyHealing(maxHealth);
        SetRemainingLives(maxLives);
    }

    public void GradualHeal()
    {
        throw new System.NotImplementedException();
    }

 

    public void ApplyHealing(float healAmount)
    {
        currentHealth = Mathf.Clamp(currentHealth + healAmount, 0, maxHealth);
        OnHealthChanged?.Invoke(currentHealth / maxHealth);
    }
    public void SetRemainingLives(int newRemainingLives)
    {
        remainingLives = Mathf.Clamp( newRemainingLives , 0, maxLives);
        OnLivesChanged?.Invoke(remainingLives);
    }
    public void SetMaxLives(int newMaxLives)
    {
        maxLives = newMaxLives - 1;
    }
    public void SetMaxHealth(float newMaxHealth)
    {
        maxHealth = newMaxHealth;
    }
}
