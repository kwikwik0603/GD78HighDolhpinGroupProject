/*
This script is for character movement -  moving, jumping, launching etc.
 */

using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class S_PlayerMovement : MonoBehaviour
{
    [Header ("Movement")]
    float horInput;
    float verInput;
    public float moveSpeed;
    public float groundDrag;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask ground;
    bool grounded;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCoolDown;
    public float airMultiplier;
    bool readyToJump = true;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    public Transform orientation;
    Vector3 moveDirection;
    Rigidbody rb;

    
    void Start()
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    
    void Update()
    {
        // Update is called once per frame
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);
        
        MyInput();
        SpeedControl();
        //FAroundandFindOut();

        if (grounded)
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = 0;
        }

        if (Input.GetKeyDown(KeyCode.L)) // Press 'L' to launch diagonally
        {
            LaunchPlayer(orientation.forward, 15f);
        }
    }

    private void MyInput()
    {

        horInput = Input.GetAxisRaw("Horizontal");
        verInput = Input.GetAxisRaw("Vertical");


        //to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCoolDown);
        }
    }

    private void MovePlayer()
    {
        //applying force to the character and moving it
        moveDirection = orientation.forward * verInput + orientation.right * horInput;
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if(!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        //limiting the speed of the character to a certain number.
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;    
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }

    }

    private void Jump()
    {
        //launch the character into the air for jumping
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        //reset the value of the readyToJump variable
        readyToJump = true;
    }

    void LaunchPlayer(Vector3 direction, float force)
    {
        rb.AddForce(direction.normalized * force, ForceMode.Impulse);
    }

    private void FAroundandFindOut()
    {
        Debug.Log(orientation.forward);
    }
}
