using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 1.0f; //these can be changed from Unity
    public float jumpStrength = 1.0f;
    bool readyToJump = true;

    public float rotationSpeed = 1.0f;
    public float verticalAngleLimit = 85.0f;

    //Ground Check
    public float playerHeight = 2;
    public LayerMask whatIsGround;
    bool grounded;

    //These are for the sprint feature
    private float lastWPressTime = 0f;
    private float doublePressInterval = 0.25f; // Interval for double press detection
    private float sprintDuration = 2.0f; // Duration for sprinting
    private float sprintTimer = 0f;
    private bool isSprinting = false;
    private float normalSpeed;

    //crouching
    public float crouchSpeed;
    public float crouchYScale;
    public float startYScale;
    public KeyCode crouchKey = KeyCode.LeftControl;


    private Vector3 currentRotation;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        normalSpeed = movementSpeed;
        startYScale = transform.localScale.y;

    }
    void Update()
    {
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        //=======Lateral movement==========
        MovePlayer();
        //======Jumping========
        Jump();
        //=======Rotation=========
        Rotate();
        // Check Y position for resetting jump count

        //crouch
        if(Input.GetKeyDown(crouchKey)) { 
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
            movementSpeed = crouchSpeed;
        }
        //stop crouch
        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            movementSpeed = normalSpeed;
        }

        /*
        //This was for the platform resetting
        if (transform.position.y == 1)
        {
            jumpCount = 0;
        }
        else if (transform.position.y <= -8.062f) // this just resets the jump count if the player hits the platform 
        {
            transform.position = new Vector3(0f, 0f, 0f); // Reset position
            jumpCount = 0; // Reset jump count as well
        }
        */
        if (isSprinting)
        {
            sprintTimer += Time.deltaTime;
            if (sprintTimer >= sprintDuration)
            {
                movementSpeed = normalSpeed; // Reset to normal speed
                isSprinting = false;
                sprintTimer = 0f;
            }
        }
    }


    void MovePlayer()
    {
       
        Vector3 direction = new Vector3(0, 0, 0);

       
        float y = rb.velocity.y;

        if (Input.GetKey(KeyCode.W))
        {
            direction += Camera.main.transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction -= Camera.main.transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction -= Camera.main.transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Camera.main.transform.right;
        }

        // Check for double press of 'W' for sprinting
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (Time.time - lastWPressTime <= doublePressInterval)
            {
                // Double press detected
                isSprinting = true;
                sprintTimer = 0f;
                movementSpeed += 2.0f; // Increase speed for sprinting
            }
            lastWPressTime = Time.time;
        }
        Vector3 velocity = direction.normalized * movementSpeed;
        velocity.y = y;
        rb.velocity = velocity;
    }

    void Jump()
    {
        //When the Space bar is pressed, apply a positive vertical force
        if (Input.GetKeyDown(KeyCode.Space) && readyToJump && grounded)
        {
            readyToJump = false;
            rb.AddForce(gameObject.transform.up * jumpStrength, ForceMode.Impulse);
            resetJump();
            
        }

    }
    void resetJump()
    {
        readyToJump = true;
    }

    void Rotate()
    {
        //Get "strength" of horizontal and verical mouse movements
        currentRotation.x += Input.GetAxis("Mouse X") * rotationSpeed;
        currentRotation.y -= Input.GetAxis("Mouse Y") * rotationSpeed;

        //X rotation is looped based on 360 degrees
        currentRotation.x = Mathf.Repeat(currentRotation.x, 360);

        //Y is clamped so the camera never flips
        currentRotation.y = Mathf.Clamp(currentRotation.y, -verticalAngleLimit, verticalAngleLimit);

        //rotate the player's view
        Camera.main.transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
    }
}
