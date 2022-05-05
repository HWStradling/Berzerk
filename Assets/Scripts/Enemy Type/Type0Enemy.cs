using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type0Enemy : IEnemyType
{
    public float PatrolSpeed { get; private set; } = 25;

    public float FollowSpeed { get; private set; } = 0;

    public float TransitionSpeed { get; private set; } = 18;

    public float AttackDelay { get; private set; } = 0;

    public float BulletSpeed { get; private set; } = 0;

    public float BulletDamage { get; private set; } = 0;
}
