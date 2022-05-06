public class Type1Enemy : IEnemyType
{
    public float PatrolSpeed { get; private set; } = 25;

    public float FollowSpeed { get; private set; } = 35;

    public float TransitionSpeed { get; private set; } = 18;

    public float AttackDelay { get; private set; } = 3;

    public float BulletSpeed { get; private set; } = 2;

    public float BulletDamage { get; private set; } = 20;
}
