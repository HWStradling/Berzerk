using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyType 
{
    public float PatrolSpeed { get; }
    public float FollowSpeed { get; }
    public float TransitionSpeed { get; }
    public float AttackDelay { get; }
    public float BulletSpeed { get; }
    public float BulletDamage { get; }
}
