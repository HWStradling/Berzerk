using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour, IAttackable
{
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private int maxLives;
    [SerializeField] private int healingDelay;
    [SerializeField] private float gradualHealAmount;
    private int healTic = 0;
    private int remainingLives;



    [SerializeField] UnityEvent<float> OnHealthChanged;
    [SerializeField] UnityEvent<int> OnLivesChanged;
    [SerializeField] UnityEvent OnDeathRespawn;
    [SerializeField] UnityEvent OnDeathFinal;
    [SerializeField] UnityEvent<int> OnEnemyDeath;
    [SerializeField] UnityEvent OnPlayerDeath;
    [SerializeField] UnityEvent OnPlayerDamaged;
    [SerializeField] UnityEvent OnHealing;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        remainingLives = maxLives - 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.CompareTag("Player"))
        {
            healTic++;
            GradualHeal();
        }

    }

    public void ApplyDamage(float damage)
    {
        if (currentHealth - damage <= 0)
        {
            currentHealth = 0;
            OnHealthChanged?.Invoke(currentHealth / maxHealth);
            Die();
        }
        else
        {
            currentHealth -= damage;
            OnHealthChanged?.Invoke(currentHealth / maxHealth);
            if (gameObject.CompareTag("Player"))
            {
                OnPlayerDamaged?.Invoke();
            }
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
            switch (remainingLives)
            {
                case 0:
                    OnDeathFinal?.Invoke();
                    OnPlayerDeath?.Invoke();
                    break;
                default:
                    Respawn();
                    break;
            }
        }

    }
    public void Respawn()
    {
        ApplyHealing(maxHealth, true);
        SetRemainingLives(remainingLives - 1);
        OnDeathRespawn?.Invoke();
        OnPlayerDeath?.Invoke();
    }
    public void Spawn()
    {
        ApplyHealing(maxHealth, true);
        SetRemainingLives(maxLives);
    }

    public void GradualHeal()
    {
        if (healTic == healingDelay * 50)
        {
            ApplyHealing(gradualHealAmount, false);
            healTic = 0;
        }
    }



    public void ApplyHealing(float healAmount, bool restore)
    {
        if (restore)
        {
            currentHealth = maxHealth;
            OnHealthChanged?.Invoke(currentHealth / maxHealth);
        }
        else if (!(currentHealth + healAmount >= maxHealth))
        {
            currentHealth += healAmount;
            OnHealthChanged?.Invoke(currentHealth / maxHealth);
            OnHealing?.Invoke();
        }



    }
    public void SetRemainingLives(int newRemainingLives)
    {
        remainingLives = Mathf.Clamp(newRemainingLives, 0, maxLives);
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
