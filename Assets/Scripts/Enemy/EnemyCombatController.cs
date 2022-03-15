using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatController : MonoBehaviour
{
    // state/type variables
    [SerializeField] private bool attackState = false;
    private int enemyType;
    private bool attackDelayed = false;
    public int directionFacingState;
    

    // enemy type 1 contstants
    [SerializeField] private float eType1AttackDelay;
    [SerializeField] private float eType1BulletSpeed;
    [SerializeField] private float eType1BulletDamage;

    // references
    [SerializeField] private GameObject[] bulletPrefabArray;
    [SerializeField] private Transform target;
    [SerializeField] private Transform firepoint;
 
    void Start()
    {
        enemyType = gameObject.GetComponent<EnemyController>().enemyType;
        directionFacingState = gameObject.GetComponent<EnemyController>().DirectionFacingState;
    }

    // Update is called once per frame
    void Update()
    {
        directionFacingState = gameObject.GetComponent<EnemyController>().DirectionFacingState;

        if (attackState && enemyType > 0 && attackDelayed == false)
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
        attackDelayed = true;
        yield return new WaitForSeconds(eType1AttackDelay);
        if (!target.gameObject.activeInHierarchy)
        {
            yield break;
        }
            
        
        Vector2 directionToPlayer = target.position - firepoint.position;
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
        attackDelayed = false;
    }
  
    public void SetAttackState(bool state)
    {
        Debug.Log(state);
        attackState = state;
    }
}
