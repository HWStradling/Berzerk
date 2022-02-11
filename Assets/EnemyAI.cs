using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public Transform player;
    public LayerMask WallLayer; // layer containing the wall's tilemap,
    public float patrolSpeed;
    public float followSpeed;
    public float transitionSpeed;
    public Transform[] patrolPattern; // array of preset patrol waypoint transform objects,

    private Animator animator;

    int currentWaypoint = 0;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ShufflePatrolPattern();
    }

    void FixedUpdate()
    { 
        // follows the player if visible, patrols if not visible.
        if (IsPlayerVisible())
        {
            FollowPlayer(); 
        }else
        {
            FollowPatrolRoute();
        }
    }
         
    
    //checks if the player is visible to the enemy, returns true if player is visible, false if not visible.
    bool IsPlayerVisible()
    {
        Vector2 enemyOrigin = rb.position;
        Vector2 playerOrigin = player.position;
        Vector2 directionToPlayer = (playerOrigin - enemyOrigin).normalized;
        float distanceToPlayer = Vector2.Distance(playerOrigin, enemyOrigin);

        // cast a ray in the direction of the player at for the exact distance the player is from the enemy,
         RaycastHit2D wallRay = Physics2D.Raycast(enemyOrigin, directionToPlayer, distanceToPlayer, WallLayer);

        // if the ray does not collide with a wall the player must be visible.
        if (wallRay.collider == null)
            return true;

        // if the ray collides with the wall, the player cannot be visible to the enemy,
        return false;
    }

    // moves the enemy towards the player at the follow speed.
    void FollowPlayer()
    {
        Vector2 enemyOrigin = rb.position;
        Vector2 playerOrigin = player.position;
        Vector2 directionToPlayer = (playerOrigin - enemyOrigin).normalized;

        // follows player then stops when the enemy has reached the player
        if (Vector2.Distance(playerOrigin, enemyOrigin) > 1)
        {
            rb.velocity = directionToPlayer * followSpeed * Time.deltaTime;
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
        Vector2 enemyOrigin = rb.position;
        Vector2 waypointOrigin = patrolPattern[currentWaypoint].position;
        Vector2 directionToWaypoint = (waypointOrigin - enemyOrigin).normalized;
        float distanceToWaypoint = Vector2.Distance(waypointOrigin, enemyOrigin);

        if (distanceToWaypoint < 1) //  if enemy has reached current waypoint,
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
            if (distanceToWaypoint < 1.5)// slows down enemy if near current waypoint,
            {
                rb.velocity = directionToWaypoint * transitionSpeed * Time.deltaTime;
                UpdateAnimation();
            }
            else
            {  // moves enemy towards current waypoint,
                rb.velocity = directionToWaypoint * patrolSpeed * Time.deltaTime;
                UpdateAnimation();
            }
        }
    }
    // randomly shuffles the patrol pattern array when called to add randomness to the patrol.
    void ShufflePatrolPattern()
    {
        for (var i = patrolPattern.Length - 1; i > 0; i--)
        {
            var rand = Random.Range(0, i);
            var temp = patrolPattern[i];
            patrolPattern[i] = patrolPattern[rand];
            patrolPattern[rand] = temp;
        }

    }
    void UpdateAnimation()
    { 
        if (Mathf.Abs(rb.velocity.y) > Mathf.Abs(rb.velocity.x))
        {
            animator.SetBool("IsMoving", true);
            if (rb.velocity.y > 0)
                animator.SetInteger("Direction", 1);
            else
                animator.SetInteger("Direction", 0);
        }else if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.y))
        {
            animator.SetBool("IsMoving", true);
            if (rb.velocity.x > 0)
                animator.SetInteger("Direction", 2);
            else
                animator.SetInteger("Direction", 3);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
        

        
    }
}
