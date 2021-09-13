using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Transform playerCam;
    [SerializeField] Transform groundCheck;
    private Rigidbody rb;

    [Header("Look Settings")]
    [Range(20, 200)]
    [SerializeField] private float sensitivity = 50f;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 5000f;
    [SerializeField] private float groundDinstance = 0.2f;
    [SerializeField] private bool grounded;
    // MAKE SURE THE PLAYER LAYER IS DIFFERENT FROM THIS LAYER MASK
    // (or it will be able to jump when in mid air)
    [SerializeField] LayerMask whatIsGround;
    private Vector2 velocity;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 550f;
    private bool readyToJump = true;
    private float jumpCooldown = 0.25f;
    private bool jumping;
    private Vector3 normalVector = Vector3.up;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 

        // Hide the cursor and make sure it doesn't exit the game window
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Handle all the input things
        jumping = Input.GetButton("Jump");
        if (jumping) Jump();

        grounded = CheckForGround();
        MovementInput();
        Look();

        // Move the player according to the input
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.y);

        // Add a down force if the player is not grounded
        if (!grounded) rb.AddForce(Vector3.down * 10);
    }

    private void MovementInput()
    {
        // Check for movement input and modify the velocity
        Vector2 x = new Vector2(
            Input.GetAxisRaw("Horizontal") * transform.right.x,
            Input.GetAxisRaw("Horizontal") * transform.right.z);
        Vector2 z = new Vector2(
            Input.GetAxisRaw("Vertical") * transform.forward.x,
            Input.GetAxisRaw("Vertical") * transform.forward.z);

        velocity = (x + z).normalized * speed * Time.deltaTime;
    }

    private void Look()
    {
        // Check for mouse input and rotate the player
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rb.rotation *= Quaternion.Euler(0, mouseX * Time.deltaTime, 0);
        playerCam.rotation *= Quaternion.Euler(-mouseY * Time.deltaTime, 0, 0);
    }

    // Check is the player is on ground
    private bool CheckForGround()
    {
        return Physics.CheckSphere(groundCheck.position, groundDinstance, whatIsGround);
    }

    private void Jump()
    {
        if (grounded && readyToJump)
        {
            readyToJump = false;

            rb.AddForce(Vector2.up * jumpForce * 1.5f);
            rb.AddForce(normalVector * jumpForce * 0.5f);

            Vector3 vel = rb.velocity;
            if (rb.velocity.y < 0.5f)
                rb.velocity = new Vector3(vel.x, 0, vel.z);
            else if (rb.velocity.y > 0)
                rb.velocity = new Vector3(vel.x, vel.y / 2, vel.z);

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

}
