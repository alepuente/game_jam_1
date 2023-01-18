using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Animator animator;
    public Transform sprite;

    // Get input from the horizontal and vertical axes
    float horizontalInput;
    float verticalInput;
    // Rigidbody component
    private Rigidbody rb;
    // Is the player grounded?
    private bool isGrounded = false;

    /*3d MOVEMENT
    float yaw;
    float pitch;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;
    */

    Vector3 moveDirection;
    public bool readyToJump;
    public bool readyToDash;

    Vector3 forward;
    Vector3 right;
    Vector3 movement;


    // Start is called before the first frame update
    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        ControllerInput();
        Movement();
        //Rotation();
    }

    private void ControllerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");


        animator.SetFloat("Horizontal", horizontalInput);
        animator.SetFloat("Vertical", verticalInput);

        if (horizontalInput == 1 || horizontalInput == -1)
        {
            Vector3 currentScale = sprite.transform.localScale;
            currentScale.x = horizontalInput;
            sprite.transform.localScale = currentScale;
        }

        if (Input.GetKey(InputController.Instance.jumpKey) && isGrounded)
        {
            Jump();
            Invoke(nameof(ResetJump), InputController.Instance.jumpCooldown);
        }

        if (Input.GetKey(InputController.Instance.dashKey) && isGrounded)
        {
            Dash();
            Invoke(nameof(ResetJump), InputController.Instance.dashCooldown);
        }

    }

    private void Movement()
    {
        /* 3D MOVEMENT
        //Move direction
         moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
         // Move the player
         rb.AddForce(moveDirection * InputController.Instance.speed * InputController.Instance.airMultiplier* Time.fixedDeltaTime, ForceMode.Force);

         Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
         if (flatVel.magnitude > InputController.Instance.speed)
         {
             Vector3 limitedVel = flatVel.normalized * InputController.Instance.speed;
             rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
         }*/

        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        rb.AddForce(moveDirection * InputController.Instance.speed * InputController.Instance.airMultiplier, ForceMode.Force);

    }

    private void Dash()
    {
        // Dash input
        rb.AddForce(transform.forward * InputController.Instance.dashDistance);
    }
    private void ResetDash()
    {
        readyToJump = true;
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * InputController.Instance.jumpForce, ForceMode.Impulse);
        // Set isGrounded to false
        isGrounded = false;
    }

    private void ResetJump()
    {
        readyToJump = true;
    }


    /*
    3D MOVEMENT
    private void Rotation()
    {
        yaw += Input.GetAxis("Mouse X") * InputController.Instance.mouseSensitivity * Time.fixedDeltaTime;
        pitch -= Input.GetAxis("Mouse Y") * InputController.Instance.mouseSensitivity * Time.fixedDeltaTime;
        pitch = Mathf.Clamp(pitch, InputController.Instance.pitchMinMax.x, InputController.Instance.pitchMinMax.y);
        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, InputController.Instance.rotationSmoothTime);
        transform.eulerAngles = currentRotation;
    }
    */

    // OnCollisionEnter is called when the player collides with a collider
    void OnCollisionEnter(Collision collision)
    {
        // Check if the player has collided with the ground
        if (collision.gameObject.tag == "Ground")
        {
            // Set isGrounded to true
            isGrounded = true;
        }
    }
}
