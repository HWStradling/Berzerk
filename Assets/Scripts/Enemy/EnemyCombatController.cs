using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatController : MonoBehaviour
{
    // state/type variables
    private bool attackState;
    private int enemyType;
    private bool allowAttackState = true;
    public int directionFacingState;
    

    // enemy type 1 contstants
    [SerializeField] private float eType1AttackDelay;
    [SerializeField] private float eType1BulletSpeed;
    [SerializeField] private float eType1BulletDamage;


    // health variables
    public float enemyCurrentHealth { get; private set; }
    public float enemyMaxHealth { get; private set; } = 100;

    // references
    [SerializeField] private GameObject[] bulletPrefabArray;
    [SerializeField] private Transform player;
    [SerializeField] private PlayerCombatController playerCombatController;
    [SerializeField] private Transform firepoint;
 
    void Start()
    {
        enemyType = gameObject.GetComponent<EnemyController>().enemyType;
        directionFacingState = gameObject.GetComponent<EnemyController>().DirectionFacingState;
        enemyCurrentHealth = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        attackState = gameObject.GetComponent<EnemyController>().attackState;
        directionFacingState = gameObject.GetComponent<EnemyController>().DirectionFacingState;

        if (attackState && enemyType > 0 && allowAttackState)
        {
            switch (enemyType) // different enemy type attacks,
            {
                case 1:
                    StartCoroutine(BasicAttack());
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    break;
            }
        }

    }
    IEnumerator BasicAttack()
    {
        GameObject bullet;
        BulletController bC;
        allowAttackState = false;
        yield return new WaitForSeconds(eType1AttackDelay);
        Vector2 directionToPlayer = player.position - firepoint.position;
        float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        switch (directionFacingState)
        {
            case 0:
                firepoint.rotation = Quaternion.AngleAxis(angleToPlayer + 90, Vector3.forward);
                bullet = Instantiate(bulletPrefabArray[0], firepoint.position, firepoint.rotation);
                bC = bullet.GetComponent<BulletController>();
                bC.Direction = 0; bC.BulletSpeed = eType1BulletSpeed; bC.Damage = eType1BulletDamage; bC.Owner = gameObject;
                break;
            case 1:
                firepoint.rotation = Quaternion.AngleAxis(angleToPlayer - 90, Vector3.forward);
                bullet = Instantiate(bulletPrefabArray[1], firepoint.position, firepoint.rotation);
                bC = bullet.GetComponent<BulletController>();
                bC.Direction = 1; bC.BulletSpeed = eType1BulletSpeed; bC.Damage = eType1BulletDamage; bC.Owner = gameObject;
                break;
            case 2:
                firepoint.rotation = Quaternion.AngleAxis(angleToPlayer + 180, Vector3.forward);
                bullet = Instantiate(bulletPrefabArray[2], firepoint.position, firepoint.rotation);
                bC = bullet.GetComponent<BulletController>();
                bC.Direction = 2; bC.BulletSpeed = eType1BulletSpeed; bC.Damage = eType1BulletDamage; bC.Owner = gameObject;
                break;
            case 3:
                firepoint.rotation = Quaternion.AngleAxis(angleToPlayer + 180, Vector3.forward);
                bullet = Instantiate(bulletPrefabArray[2], firepoint.position, firepoint.rotation);
                bC = bullet.GetComponent<BulletController>();
                bC.Direction = 3; bC.BulletSpeed = eType1BulletSpeed; bC.Damage = eType1BulletDamage; bC.Owner = gameObject;
                break;
            default:
                break;
        }
        allowAttackState = true;
    }
    public void ApplyDamage(float damage)
    {
        Debug.Log("apply Damage");
        if (enemyCurrentHealth - damage <= 0)
        {
            enemyCurrentHealth = 0;
            Die();
        }
        else
        {
            enemyCurrentHealth -= damage;
            // play damage animation
        }
    }
    private void Die()
    {
        // death animation,
        Destroy(gameObject);
    }
}
