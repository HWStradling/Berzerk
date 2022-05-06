
public interface IAttackable
{

    public void ApplyDamage(float damage);
    public void ApplyHealing(float healAmount, bool restore);
    public void Die();
    public void GradualHeal();
    public void SetMaxHealth(float newMaxHealth);
}
