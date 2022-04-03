using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{

    private Transform targetTransform;
    [SerializeField] private LayerMask WallLayer; // layer containing the wall's tilemap,
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float patrolSpeed;
    [SerializeField] private float followSpeed;
    [SerializeField] private float transitionSpeed;
    [SerializeField] private Transform[] patrolPattern;// array of preset patrol waypoint transform objects,

    [SerializeField] public int enemyType;
    [SerializeField] UnityEvent<bool> OnAttacking;
    private bool attackState = false;
    public int DirectionFacingState { get; private set; } = 0;



    private int currentWaypoint = 0;
    Vector2 enemyOrigin;
    Vector2 targetOrigin;
    Vector2 directionToTarget;
    float distanceToTarget;




    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Player") != null)
        {
            targetTransform = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        }
        ShufflePatrolPattern();
    }

    void FixedUpdate()
    {
        // follows the player if visible, patrols if not visible.
        if (targetTransform != null)
        {
            if (IsTargetVisible() && enemyType > 0)
                FollowTarget();
            else
                FollowPatrolRoute();
        }
    }
         
    
    //checks if the player is visible to the enemy, returns true if player is visible, false if not visible.
    bool IsTargetVisible()
    {
        SetDirDist(targetTransform);

        // cast a ray in the direction of the player at for the exact distance the player is from the enemy,
        RaycastHit2D wallRay = Physics2D.Raycast(enemyOrigin, directionToTarget, distanceToTarget, WallLayer);

        // if the ray does not collide with a wall the player must be visible.
        if (wallRay.collider == null && targetTransform.gameObject.activeInHierarchy)
        {
            attackState = true;
            OnAttacking?.Invoke(attackState);
            return attackState;
        }


        // if the ray collides with the wall, the player cannot be visible to the enemy,
        attackState = false;
        OnAttacking?.Invoke(attackState);
        return attackState;
    }

    // moves the enemy towards the player at the follow speed.
    void FollowTarget()
    {
        SetDirDist(targetTransform);

        // follows player then stops when the enemy has reached the player
        if (Vector2.Distance(targetOrigin, enemyOrigin) > 1.5)
        {
            rb.velocity = followSpeed * Time.fixedDeltaTime * directionToTarget;
            UpdateAnimation();         
        }
        else
        {
            rb.velocity = Vector2.zero;
            UpdateAnimation();
        }


    }
    // moves the enemy towards the next waypoint in the patrol pattern,
    // reshuffles the patrol pattern if the enemy has completed it,
    void FollowPatrolRoute()
    {
        SetDirDist(patrolPattern[currentWaypoint]);

        if (distanceToTarget < 1) //  if enemy has reached current waypoint,
        {
            if (currentWaypoint == patrolPattern.Length - 1) //  if enemy has reached the end of the patrol pattern,
            {
                currentWaypoint = 0;
                ShufflePatrolPattern(); // randomizes the patrol pattern array so the enemy does not repeat the same pattern,
                return;
            }
            else
            {
                currentWaypoint++; // set new waypoint,
                UpdateAnimation();
            }
                
        }
        else // enemy not at current waypoint,
        {
            if (distanceToTarget < 1.5)// slows down enemy if near current waypoint,
            {
                rb.velocity = Time.deltaTime * transitionSpeed * directionToTarget;
                UpdateAnimation();
            }
            else
            {  // moves enemy towards current waypoint,
                rb.velocity = patrolSpeed * Time.deltaTime * directionToTarget;
                UpdateAnimation();
            }
        }
    }
    // shuffles patrol pattern when colliding with another enemy to prevent them hanging up,
     void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!attackState)
            {
                ShufflePatrolPattern();
            }
            
        }
    }
    // randomly shuffles the patrol pattern array when called to add randomness to the patrol.
    private void ShufflePatrolPattern()
    {
        for (var i = patrolPattern.Length - 1; i > 0; i--)
        {
            var rand = Random.Range(0, i);
            var temp = patrolPattern[i];
            patrolPattern[i] = patrolPattern[rand];
            patrolPattern[rand] = temp;
        }

    }
     private void UpdateAnimation()
    { 
        if (Mathf.Abs(rb.velocity.y) > Mathf.Abs(rb.velocity.x))
        {
            animator.SetBool("IsMoving", true);
            if (rb.velocity.y > 0)
            {
                animator.SetInteger("Direction", 1);
                DirectionFacingState = 1;
            }
            else
            {
                animator.SetInteger("Direction", 0);
                DirectionFacingState = 0;
            }
        }else if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.y))
        {
            animator.SetBool("IsMoving", true);
            if (rb.velocity.x > 0)
            {
                animator.SetInteger("Direction", 2);
                DirectionFacingState = 2;
            }
            else
            {
                animator.SetInteger("Direction", 3);
                DirectionFacingState = 3;
            }
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

}

    private void SetDirDist(Transform target)
    {
        enemyOrigin = rb.position;
        targetOrigin = target.position;
        directionToTarget = (targetOrigin - enemyOrigin).normalized;
        distanceToTarget = Vector2.Distance(targetOrigin, enemyOrigin);
    }
}