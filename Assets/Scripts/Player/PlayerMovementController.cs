using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float sprintingSpeed;
    /*[SerializeField] private float aimingArc;
    [SerializeField] private Transform arm;
    [SerializeField] private Transform weaponHandle;
*/// private new Camera camera;

    private float horizontalInput = 0f;
    private float verticalInput = 0f;
    private Animator animator;
    private Vector2 directionOfTravel = Vector2.zero;
    public int directionFacingState = 0;
    private Rigidbody2D rb;
    private bool isSprinting;



    public UnityEvent<int> OnDirectionChange;

    public int maxLevel = 1;

    private void Awake()
    {
        maxLevel = SaveSystem.PlayerData.MaxLevel;
    }
    private void Start()
    {
        MoveToSpawnPoint();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        MoveToSpawnPoint();
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentBuildIndex > maxLevel)
        {
            maxLevel = currentBuildIndex;
        }
    }

    private void Update()
    {
        // gets the raw inputs and stores them in a vector2,
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        directionOfTravel = new Vector2(horizontalInput, verticalInput);
    }


    private void FixedUpdate()
    {
        bool directionChanged = false;
        isSprinting = Input.GetKey(KeyCode.LeftShift) && directionOfTravel != Vector2.zero;
        // switch for setting the animator state for left and right directions,
        switch (directionOfTravel.x)
        {
            case 1:
                transform.rotation = new Quaternion(0f, 180f, 0f, 1);
                animator.SetInteger("Direction", 2);
                directionFacingState = 2;
                directionChanged = true;
                break;
            case -1:
                transform.rotation = new Quaternion(0f, 0f, 0f, 1);
                animator.SetInteger("Direction", 3);
                directionFacingState = 3;
                directionChanged = true;
                break;
        }
        // switch for setting the animator state for up and down directions,
        switch (directionOfTravel.y)
        {
            case 1:
                transform.rotation = new Quaternion(0f, 0f, 0f, 1);
                animator.SetInteger("Direction", 1);
                directionFacingState = 1;
                directionChanged = true;
                break;
            case -1:
                transform.rotation = new Quaternion(0f, 0f, 0f, 1);
                animator.SetInteger("Direction", 0);
                directionFacingState = 0;
                directionChanged = true;
                break;
        }

        if (directionChanged) // for efficiency
        {
            OnDirectionChange?.Invoke(directionFacingState);
            directionChanged = false;
        }



        // updating the animator with the correct parameters for Walking and Sprinting,
        animator.SetBool("Walking", directionOfTravel.y != 0 || directionOfTravel.x != 0);
        animator.SetBool("Sprinting", isSprinting);
        // moves the player,
        switch (Input.GetKey(KeyCode.LeftShift) && directionOfTravel != Vector2.zero)
        {
            case false:
                rb.velocity = movementSpeed * Time.deltaTime * directionOfTravel;
                break;
            case true:
                rb.velocity = sprintingSpeed * Time.deltaTime * directionOfTravel;
                break;
        }
    }
    public void MoveToSpawnPoint()
    {
        Transform spawnPoint = GameObject.FindGameObjectWithTag("spawn_point").transform;
        if (spawnPoint == null)
        {
            Debug.Log("spawnPoint null");
        }
        else
        {
            transform.position = spawnPoint.position;
        }
    }
    public void onDeathFinal()
    {
        ExitToMenu.DestroyForMenu();
        SceneManager.LoadScene(0);
    }

}