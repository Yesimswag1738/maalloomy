using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{
    public float slideForceMultiplier = 1.0f;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float sprintMultiplier = 2f;
    public float slideDuration = 1f;
    public KeyCode slideKey = KeyCode.LeftShift;
    public KeyCode sprintKey = KeyCode.Space;

    private Rigidbody rb;
    private bool isGrounded = true;
    private bool isSliding = false;
    private float slideTimer = 1f;
    public float gravity = -20f;
    // PUBLIC ANIM RUN;
    
    //Vector3 velocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Move player
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        float speed = moveSpeed;
        if (Input.GetKey(sprintKey))
        {
            speed *= sprintMultiplier;
        }
        if (isSliding)
        {
            speed = 2f;
        }
        rb.MovePosition(rb.position + transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        
        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            //velocity.y += gravity * Time.deltaTime;
            //Physics.gravity = new Vector3(0, -90.0F, 0);
        }
      

        // Slide
        if (Input.GetKeyDown(slideKey) && !isSliding && isGrounded)
        {
            isSliding = true;
            slideTimer = slideDuration;

            // Apply force in the direction of movement
            Vector3 slideForce = new Vector3(horizontalInput, 0f, verticalInput).normalized * rb.mass * -moveSpeed * 0.25f * slideForceMultiplier;
            rb.AddForce(slideForce, ForceMode.Impulse);
        }

        // Update slide timer
        if (isSliding)
        {
            slideTimer -= Time.deltaTime;
            if (slideTimer <= 0f)
            {
                isSliding = false;

            }
        }

        if (!isGrounded)
        {
            Physics.gravity = new Vector3(0, gravity, 0 * Time.deltaTime);
        }
        else
        {
            Physics.gravity = new Vector3(0, -9.0F, 0);
        }

        
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if player is grounded
        if (collision.gameObject.CompareTag("GROUND"))
        {
            isGrounded = true;
        }
    }
}