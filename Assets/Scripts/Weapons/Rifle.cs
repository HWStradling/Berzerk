public class Rifle : IWeapon
{
    public float BulletDelay { get; private set; } = 0.3f;
    public float BulletSpeed { get; private set; } = 4.5f;
    public float WeaponDamage { get; private set; } = 20f;
}
