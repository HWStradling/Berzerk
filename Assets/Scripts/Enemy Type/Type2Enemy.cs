public class Type2Enemy : IEnemyType
{
    public float PatrolSpeed { get; private set; } = 25f;

    public float FollowSpeed { get; private set; } = 60f;

    public float TransitionSpeed { get; private set; } = 18f;

    public float AttackDelay { get; private set; } = 1f;

    public float BulletSpeed { get; private set; } = 3f;

    public float BulletDamage { get; private set; } = 25f;
}
