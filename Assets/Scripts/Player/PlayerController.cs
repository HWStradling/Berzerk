using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float sprintingSpeed;
    [SerializeField] private float aimingArc;
    [SerializeField] private Transform arm;
    [SerializeField] private Transform weaponHandle;
    

    private float horizontalInput = 0f;
    private float verticalInput = 0f;
    private Animator animator;
    private Vector2 directionOfTravel = Vector2.zero;
    public int DirectionFacingState { get; private set; } = 0;
    private Rigidbody2D rb;
    private bool isSprinting;
    private new Camera camera;
    

    private void Start()
    {
        camera = Camera.main;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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
        UpdateGunRotation();
        isSprinting = Input.GetKey(KeyCode.LeftShift) && directionOfTravel != Vector2.zero;
        // switch for setting the animator state for left and right directions,
        switch (directionOfTravel.x)
        {
            case  1:
                transform.rotation = new Quaternion(0f, 180f, 0f, 1);
                animator.SetInteger("Direction", 2);
                DirectionFacingState = 2;
                break;
            case -1:
                transform.rotation = new Quaternion(0f, 0f, 0f, 1);
                animator.SetInteger("Direction", 3);
                DirectionFacingState = 3;
                break;
        }
        // switch for setting the animator state for up and down directions,
        switch (directionOfTravel.y)
        {
            case 1:
                transform.rotation = new Quaternion(0f, 0f, 0f, 1);
                animator.SetInteger("Direction", 1);
                DirectionFacingState = 1;
                break;
            case -1:
                transform.rotation = new Quaternion(0f, 0f, 0f, 1);
                animator.SetInteger("Direction", 0);
                DirectionFacingState = 0;
                break;
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
    private void UpdateGunRotation()
    {
        if (arm.gameObject.activeSelf || weaponHandle.gameObject.activeSelf)
        {
            Vector2 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 directionToMouse = mousePos - (Vector2)arm.position;
            float angleToMouse = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
            switch (DirectionFacingState)// TODO: make directionfacingstate an enum.
            {
                case 0:// up,
                    arm.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    weaponHandle.localRotation = Quaternion.Euler(0f, 0f, Mathf.Clamp(angleToMouse, - 90 - aimingArc, -90 + aimingArc) + 90); // use different method !!!
                    return;
                case 1: //up
                    arm.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    weaponHandle.localRotation = Quaternion.Euler(0f, 0f, Mathf.Clamp(angleToMouse,  90 - aimingArc, 90 + aimingArc) -90);
                    return;
                case 2:// right flip,
                    weaponHandle.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    arm.localRotation = Quaternion.Euler(0f, 0f, - Mathf.Clamp(angleToMouse, -aimingArc, aimingArc));
                    break;
                case 3: // left,

                    if (angleToMouse > 0)
                    {
                        weaponHandle.localRotation = Quaternion.Euler(0f, 0f, 0f);
                        arm.localRotation = Quaternion.Euler(0f, 0f, Mathf.Clamp(angleToMouse, 180 - aimingArc, 180) -180 );
                    }
                    if (angleToMouse < 0)
                    {
                        weaponHandle.localRotation = Quaternion.Euler(0f, 0f, 0f);
                        arm.localRotation = Quaternion.Euler(0f, 0f, Mathf.Clamp(angleToMouse, -180 , -180 + aimingArc ) + 180);
                    }
                    break;
                default:
                    break;
            }
        }
        
    }
    public int GetDirectionFacing()
    {
        return DirectionFacingState;
    }
}