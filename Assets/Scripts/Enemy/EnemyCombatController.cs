using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatController : MonoBehaviour
{
    // state/type variables
    [SerializeField] private bool attackState = false;
    private bool attackDelayed = false;
    public int directionFacingState;

    [SerializeField]private int enemyType;
    private IEnemyType enemyTypeData;

    // references
    [SerializeField] private GameObject[] bulletPrefabArray;
    private Transform targetTransform;
    [SerializeField] private Transform firepoint;
 
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length > 0)
        {
            targetTransform = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        }
        switch (enemyType)
        {
            case 0:
                enemyTypeData = new Type0Enemy();
                break;
            case 1:
                enemyTypeData = new Type1Enemy();
                break;
            default:
                break;
        }
        directionFacingState = gameObject.GetComponent<EnemyController>().DirectionFacingState;
    }

    // Update is called once per frame
    void Update()
    {
        directionFacingState = gameObject.GetComponent<EnemyController>().DirectionFacingState;

        if (attackState && enemyType > 0 && attackDelayed == false && targetTransform!= null)
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
        attackDelayed = true;
        yield return new WaitForSeconds(enemyTypeData.AttackDelay);
        if (!targetTransform.gameObject.activeInHierarchy || !attackState)
        {
            yield break;
        }
        Vector2 directionToPlayer = targetTransform.position - firepoint.position;
        float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        switch (directionFacingState)
        {
            case 0:
                firepoint.rotation = Quaternion.AngleAxis(angleToPlayer + 90, Vector3.forward);
                SetupBullet(0, 0);
                break;
            case 1:
                firepoint.rotation = Quaternion.AngleAxis(angleToPlayer - 90, Vector3.forward);
                SetupBullet(1, 1);
                break;
            case 2:
                firepoint.rotation = Quaternion.AngleAxis(angleToPlayer + 180, Vector3.forward);
                SetupBullet(2, 2);
                break;
            case 3:
                firepoint.rotation = Quaternion.AngleAxis(angleToPlayer + 180, Vector3.forward);
                SetupBullet(2, 3);
                break;
            default:
                break;
        }
        
        attackDelayed = false;
    }

    private void SetupBullet(int bulletPrefabIndex, int direction)
    {
        GameObject bullet = Instantiate(bulletPrefabArray[bulletPrefabIndex], firepoint.position, firepoint.rotation);
        BulletController bC = bullet.GetComponent<BulletController>();
        bC.Direction = direction;
        bC.BulletSpeed = enemyTypeData.BulletSpeed; 
        bC.Damage = enemyTypeData.BulletDamage; 
        bC.Owner = gameObject;

    }
  
    public void SetAttackState(bool state)
    {
        Debug.Log("Enemy Attack State: " + state);
        attackState = state;
    }
}
