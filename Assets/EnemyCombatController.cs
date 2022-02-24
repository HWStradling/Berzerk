using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatController : MonoBehaviour
{
    private bool attackState;
    private int enemyType;
    [SerializeField] private Transform playerPos;
    [SerializeField] private CombatController playerCombatController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attackState = gameObject.GetComponent<EnemyAI>().attackState;
        enemyType = gameObject.GetComponent<EnemyAI>().enemyType;

        if (attackState && enemyType > 0)
        {
            switch (enemyType) // different enemy type attacks,
            {
                case 1:
                    BasicAttack();
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
    private void BasicAttack()
    {

    }
}
