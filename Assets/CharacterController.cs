using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float movementSpeed;
    public float sprintingSpeed;

    private float horizontalInput = 0f;
    private float verticalInput = 0f;
    private Animator animator;
    private Vector2 direction = Vector2.zero;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // gets the raw inputs and stores them in a vector2,
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        direction = new Vector2(horizontalInput, verticalInput);
    }

    private void FixedUpdate()
    {
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        // switch for setting the animator state for left and right directions,
        switch (direction.x)
        {
            case  1:
                animator.SetInteger("Direction", 2);
                sr.flipX = true; // flips sprite to allow left and right movement,
                break;
            case -1:
                animator.SetInteger("Direction", 3);
                sr.flipX = false;
                break;
        }
        // switch for setting the animator state for up and down directions,
        switch (direction.y)
        {
            case 1:
                animator.SetInteger("Direction", 1);
                sr.flipX = false;
                break;
            case -1:
                animator.SetInteger("Direction", 0);
                sr.flipX = false;
                break;
        }

        // updating the animator with the correct parameters for Walking and Sprinting,
        animator.SetBool("Walking", direction.y != 0 || direction.x != 0);
        animator.SetBool("Sprinting", isSprinting);
       
        // moves the player,
        switch (Input.GetKey(KeyCode.LeftShift))
        {
            case false:
                rb.velocity = movementSpeed * direction * Time.deltaTime;
                break;
            case true:
                rb.velocity = sprintingSpeed * direction * Time.deltaTime;
                break;
        }
       
    }
}

