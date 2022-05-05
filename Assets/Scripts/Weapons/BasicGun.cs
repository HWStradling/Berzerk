using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGun : IWeapon
{
    public float BulletDelay { get; private set; } = 1f;
    public float BulletSpeed { get; private set; } = 3f;
    public float WeaponDamage { get; private set; } = 20f;

}
