
public interface IAttackable
{

    public void ApplyDamage(float damage);
    public void ApplyHeal(float healAmount);
    public void Die();
    public void GradualHeal();
    public void SetMaxHealth(float newMaxHealth);
}
