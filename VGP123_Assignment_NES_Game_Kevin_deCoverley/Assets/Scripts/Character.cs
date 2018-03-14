using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    // object Instantiate(object prefab, Vector3 position, Quaternion rotation);

    // Method 1: Reference Rigidbody2D through script
    // - Not shown in Inspector
    Rigidbody2D rb;

    // Method 2: Reference Rigidbody2D through script
    // - Shown in Inspector
    public Rigidbody2D rb2;

    // Handle movement speed of Character
    // - Can be adjusted through Inspector while in Play mode
    public float speed;
    public bool lookUp; // Player is pressing up
    public bool jumping; // player presses jump
    public bool shooting = false; // player shoots
    // Handles jump speed of Character
    public float jumpForce;             // How high the character will Jump
    public bool isGrounded = true;             // Is the player touching the ground?
    public LayerMask isGroundLayer;     // What is the Ground? Player can only jump on things that are on the "Ground" layer  
    public Transform groundCheck;       // Used to figure out if the player is touching the ground
    public float groundCheckRadius;     // Size of circle around empty GameObject
    public bool isFacingLeft = false;
    // Handles animations
    public Animator anim;

    public float projectileForce;
    public Transform projSpawn;
    public BaseBallProjScript projectilePrefab;

    // Use this for initialization
    void Start () {

        // Method 1: Reference Rigidbody through script
        rb = GetComponent<Rigidbody2D>();

        // Checks if Component exists
        if (!rb)
        {
            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogWarning("No Rigidbody2D found.");
        }

        // Checks if Component exists
        if (!rb2)
        {
            // Prints a message to Console (Shortcut: Control+Shift+C)
            rb2 = GetComponent<Rigidbody2D>();
        }

        // Check if speed was set to something not 0
        if (speed <= 0)
        {
            // Assign a default value if one is not set in the Inspector
            speed = 5.0f;

            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogWarning("Default speed to " + speed);
        }

        // Check if speed was set to something not 0
        if (jumpForce == 0)
        {
            // Assign a default value if one is not set in the Inspector
            jumpForce = 5.0f;

            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.Log("Jump Force was not set. Defaulting to " + jumpForce);
        }

        // Checks if GroundCheck GameObject is connected
        if (!groundCheck)
        {
            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogError("No Ground Check found on GameObject");
        }

        // Check if speed was set to something not 0
        if (groundCheckRadius == 0)
        {
            // Assign a default value if one is not set in the Inspector
            groundCheckRadius = 0.2f;

            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.Log("Ground Check Radius was not set. Defaulting to " + groundCheckRadius);
        }

        // Reference Component through script
        anim = GetComponent<Animator>();

        // Checks if Component exists
        if (!anim)
        {
            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogError("No Animator found on GameObject");
        }
    }

    void flip()
    {
        isFacingLeft = !isFacingLeft;

        Vector3 scaleFactor = transform.localScale;
        scaleFactor.x = -scaleFactor.x;
        transform.localScale = scaleFactor;
        anim.SetBool("isFacingLeft", isFacingLeft);
    }

    void fire()
    {
        if (isFacingLeft == false)
        {
            BaseBallProjScript temp = Instantiate(projectilePrefab, projSpawn.position, projSpawn.rotation);
            temp.speed = projectileForce;
            Debug.LogWarning("BallSpawned");
        }
        else
        {
            BaseBallProjScript temp = Instantiate(projectilePrefab, projSpawn.position, projSpawn.rotation);
            temp.speed = -projectileForce;
            Debug.LogWarning("BallSpawned");
        }
    }
    
    // Update is called once per frame
    void Update()
    {

        // Check if groundCheck GameObject is touching something tagged as Ground or Platform
        // - Can change groundCheckRadius to a smaller value if you need more precision or if the sizing of the Character is small
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius,
            isGroundLayer);
        // Relay the isGrounded variable to the animator
        anim.SetBool("isGrounded", isGrounded);


        // Checks if Left (or a) or Right (or d) is pressed
        // - "Horizontal" must exist in Input Manager (Edit-->Project Settings-->Input)
        // - Returns -1(left), 1(right), 0(nothing)
        // - Use GetAxis for value -1-->0-->1 and all decimal places. (Gradual change in values)
        float moveValue = Input.GetAxisRaw("Horizontal");

        // Check if "Jump" button was pressed
        // - "Jump" must exist in Input Manager (Edit-->Project Settings-->Input)
        // - Configuration can be changed later
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.Log("Jump");

            // Relay the isGrounded variable to the animator
            anim.SetBool("isGrounded", isGrounded);

            // Vector2.up --> new Vector2(0,1);
            // Vector2.down --> new Vector2(0,-1);
            // Vector2.right --> new Vector2(1,0);
            // Vector2.left --> new Vector2(-1,0);

            // Applies a force in the UP direction
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // Check if Left Control was pressed\
        // - Tied to key and cannot be changed
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.Log("Pew pew");
            shooting = true; // player is looking up
            anim.SetBool("shooting", shooting);
            fire();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            shooting = false; // reset the animation
            anim.SetBool("shooting", shooting);
        }
        // Move Character using Rigidbody2D
        // - Uses moveValue from GetAxis to move left or right
        rb.velocity = new Vector2(moveValue * speed, rb.velocity.y);

        // Tells Animator to transition to another Clip
        // - Parameter must be created in Animator window
        anim.SetFloat("MoveValue", Mathf.Abs(moveValue));

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            lookUp = true; // player is looking up
            anim.SetBool("lookUp", lookUp);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            lookUp = false; // player is not looking up
            anim.SetBool("lookUp", lookUp);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumping = true; // player is jumping
            anim.SetBool("jumping", jumping);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumping = false; // player is not jumping
            anim.SetBool("jumping", jumping);
        }

        //switch sides to Left/right
        if (Input.GetKeyDown(KeyCode.LeftArrow) && isFacingLeft == false || Input.GetKeyDown(KeyCode.RightArrow) && isFacingLeft == true)
        {
            flip();
        }
        /* Unneccesary ATM
        if (projectilePrefab == null)
        {
            Debug.LogError("Null");
        }
        if (projSpawn == null)
        {
            Debug.LogError("Null");
        }
        if (projectileForce == 0)
        {
            projectileForce = 7.0f;
            Debug.LogError("Auto set ot 7");
        }
        */
    }
}
