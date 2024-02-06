using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 1.0f;
    public float jumpStrength = 1.0f;

    private int jumpCount = 0;
    private const int MaxJumpCount = 1;

    public float rotationSpeed = 1.0f;
    public float verticalAngleLimit = 85.0f;

    private float lastWPressTime = 0f;
    private float doublePressInterval = 0.25f; // Interval for double press detection
    private float sprintDuration = 2.0f; // Duration for sprinting
    private float sprintTimer = 0f;
    private bool isSprinting = false;
    private float normalSpeed;


    private Vector3 currentRotation;
    Rigidbody rb;

    void Start()
    {
        //Grab the rigidbody we want to manipulate for movement
        rb = GetComponent<Rigidbody>();
        normalSpeed = movementSpeed;

    }
    void Update()
    {
        //=======Lateral movement==========
        MovePlayer();
        //======Jumping========
        Jump();
        //=======Rotation=========
        Rotate();
        // Check Y position for resetting jump count
        if (transform.position.y == 1)
        {
            jumpCount = 0;
        }
        else if (transform.position.y <= -8.062f)
        {
            transform.position = new Vector3(0f, 0f, 0f); // Reset position
            jumpCount = 0; // Reset jump count as well
        }
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


        //Get the normalized vector, then scale based on the current speed
        //Q4) Why do we need to normalize here?
        Vector3 velocity = direction.normalized * movementSpeed;


        //Add back the y component
        velocity.y = y;
        //apply the velocity to the player
        rb.velocity = velocity;
    }

    void Jump()
    {
        //When the Space bar is pressed, apply a positive vertical force
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < MaxJumpCount)
        {
            rb.AddForce(gameObject.transform.up * jumpStrength, ForceMode.Impulse);
            jumpCount++;
        }

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
