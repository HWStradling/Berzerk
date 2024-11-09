using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{

    private Transform targetTransform;
    [SerializeField] private LayerMask WallLayer; // layer containing the wall's tilemap,
    private Animator animator;
    private Rigidbody2D rb;

    /*[SerializeField] private float patrolSpeed;
    [SerializeField] private float followSpeed;
    [SerializeField] private float transitionSpeed;*/
    [SerializeField] private Transform[] patrolPattern;// array of preset patrol waypoint transform objects,

    [SerializeField] public int enemyType;
    private IEnemyType enemyTypeData;
    [SerializeField] UnityEvent<bool> OnAttacking;
    private bool attackState = false;
    private bool findingState = false;

    public int DirectionFacingState { get; private set; } = 0;

    private int currentWaypoint = 0;
    Vector2 enemyOrigin;
    Vector2 targetOrigin;
    Vector2 directionToTarget;
    float distanceToTarget;
    float lastKnownDistanceToTarget;
    float followTimer;

    void Start()
    {
        switch (enemyType)
        {
            case 0:
                enemyTypeData = new Type0Enemy();
                break;
            case 1:
                enemyTypeData = new Type1Enemy();
                break;
            case 2:
                enemyTypeData = new Type2Enemy();
                break;
            default:
                break;
        }

        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        if (GameObject.FindGameObjectsWithTag("Player").Length > 0)
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
            // state where target not visible but attack state not updated yet (target just lost),
            // or findingState and target not visible (finding but not found),
            if (attackState && !IsTargetVisible())
            {

                findingState = true;
                attackState = false;
                OnAttacking?.Invoke(attackState);
            }
            //Debug.Log("finding state: " + findingState);
            if (IsTargetVisible() && enemyType > 0)
                FollowTarget();
            else if ((!IsTargetVisible() && !findingState) || enemyType == 0)
                FollowPatrolRoute();
            else
                FindTarget();
        }
    }


    //checks if the player is visible to the enemy, returns true if player is visible, false if not visible.
    bool IsTargetVisible()
    {
        SetDirDist(targetTransform, false);

        // cast a ray in the direction of the player at for the exact distance the player is from the enemy,
        RaycastHit2D wallRay = Physics2D.Raycast(enemyOrigin, directionToTarget, distanceToTarget, WallLayer);

        // if the ray does not collide with a wall the player must be visible.
        if (wallRay.collider == null && targetTransform.gameObject.activeInHierarchy)
        {
            return true;
        }
        // if the ray collides with the wall, the player cannot be visible to the enemy,
        return false;
    }

    // moves the enemy towards the player at the follow speed.
    void FollowTarget()
    {
        findingState = false;
        attackState = true;
        OnAttacking?.Invoke(attackState);

        SetDirDist(targetTransform, true);

        // follows player then stops when the enemy has reached the player
        if (distanceToTarget > 1.5)
        {
            rb.velocity = enemyTypeData.FollowSpeed * Time.fixedDeltaTime * directionToTarget;
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
        attackState = false;
        OnAttacking?.Invoke(attackState);
        if (patrolPattern.Length == 0)
        {
            return;
        }

        SetDirDist(patrolPattern[currentWaypoint], false);

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
                rb.velocity = Time.deltaTime * enemyTypeData.TransitionSpeed * directionToTarget;
                UpdateAnimation();
            }
            else
            {  // moves enemy towards current waypoint,
                rb.velocity = enemyTypeData.PatrolSpeed * Time.deltaTime * directionToTarget;
                UpdateAnimation();
            }
        }
    }
    // continues following vector towards where player was last seen,

    void FindTarget()
    {
        // if enemy reaches last known player location and does not find player,
        // sets finding state to false, (starts normal patrol or follow behavior)
        if (lastKnownDistanceToTarget < 5 || followTimer == 250)
        {
            findingState = false;
            followTimer = 0;
        }
        else
        {
            followTimer++;
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
        }
        else if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.y))
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

    private void SetDirDist(Transform target, bool attacking)
    {
        enemyOrigin = rb.position;
        targetOrigin = target.position;
        directionToTarget = (targetOrigin - enemyOrigin).normalized;
        distanceToTarget = Vector2.Distance(targetOrigin, enemyOrigin);
        if (attacking)
        {
            lastKnownDistanceToTarget = distanceToTarget;
        }
    }
}