using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement")]

    // MoveSpeed
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;



    // Stamina
    static public float staminapoints = 100;
    public bool isSprinting;
    public float staminadrain = 1;
    public float staminaregen = 1;
    public bool isRegenerating;

    // Jump
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    // Crouch
    public float crouchSpeed;
    public float crouchingWhileSprinting;
    public float crouchYScale;
    private float normalYScale;


    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;
    bool movement;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;

    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        crouchingsprinting,
        air
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        readyToJump = true;
        normalYScale = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        // Funcions Update
        Inputs();
        SpeedControl();
        StateHandler();

        // Drag
        if (grounded)
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = 0;
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
    }
    // Inputs
    void Inputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Is jump possible
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(JumpReset), jumpCooldown);
        }

        // Start crouching
        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, normalYScale, transform.localScale.z);

        }
    }

    void StateHandler()
    {
        // Crouching
        if (grounded && Input.GetKey(crouchKey) && !Input.GetKey(sprintKey))
        {
            state = MovementState.crouching;
        }
        // Crouch + Sprint
        else if (grounded && Input.GetKey(crouchKey) && Input.GetKey(sprintKey) && staminapoints > 0)
        {
            state = MovementState.crouchingsprinting;
        }
        // Walking
        else if (grounded && !Input.GetKey(crouchKey) && !Input.GetKey(sprintKey))
        {
            state = MovementState.walking;
        }
        // Sprinting
        else if (grounded && Input.GetKey(sprintKey) && !Input.GetKey(crouchKey) && staminapoints > 0)
        {
            state = MovementState.sprinting;
        }
        else if (grounded && Input.GetKey(sprintKey) && !Input.GetKey(crouchKey) && staminapoints < 0)
        {
            state = MovementState.walking;
        }
        else if (grounded && Input.GetKey(sprintKey) && Input.GetKey(crouchKey) && staminapoints < 0)
        {
            state = MovementState.crouching;
        }
        else
        {
            state = MovementState.air;
        }

        // Update moveSpeed based on state
        switch (state)
        {
            case MovementState.crouching:
                moveSpeed = crouchSpeed;
                break;
            case MovementState.crouchingsprinting:
                moveSpeed = crouchingWhileSprinting;
                break;
            case MovementState.walking:
                moveSpeed = walkSpeed;
                break;
            case MovementState.sprinting:
                moveSpeed = sprintSpeed;
                break;
            case MovementState.air:
                break;
        }

        // Sprintcheck
        if (grounded && Input.GetKey(sprintKey))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }

        // Staminapoints
        if ((rb.linearVelocity.x > 1 || rb.linearVelocity.z > 1) && Input.GetKey(sprintKey) && staminapoints > 0)
        {
            staminapoints -= staminadrain * Time.deltaTime * 5;
        }

        // Stamina regeneration
        if (!Input.GetKey(sprintKey))
        {
            if (!isRegenerating)
            {
                InvokeRepeating("StaminaRegen", 4, 0.04f);
                isRegenerating = true;
            }
        }
        else
        {
            CancelInvoke("StaminaRegen");
            isRegenerating = false;
        }
    }

    void StaminaRegen()
    {
        if (staminapoints < 100)
        {
            staminapoints += staminaregen * Time.deltaTime * 10;
        }
        else
        {
            CancelInvoke("StaminaRegen");
            isRegenerating = false;
        }
    }


    // Movement calculation and adjustments
    void MovePlayer()
    {
        // Direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Ground
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }

        // Air
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }
    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        // Velocity limiter
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }

    }
    void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    void JumpReset()
    {
        readyToJump = true;
    }
}
