using UnityEngine;

/// <summary>
/// A basic NPC AI system
/// </summary>
public class WalkingNPC : Sign
{
    // ^^ Inherits from Sign because Sign has the dialogue system set up
    // and Sign inherits from Interactable, making the NPC interactable by the player,
    // and NPC will be able to have dialogue without writing a whole new system

    [Header("Settings")]
    public float walkSpeed;             // NPC's movement speed
    public float minMoveTime;           // Minimum move time
    public float maxMoveTime;           // Maximum move time
    public float minWaitTime;           // Minimum wait time
    public float maxWaitTime;           // Maximum wait time
    public Collider2D boundary;         // NPC's movement boundary

    // Privates
    private bool isMoving;              // Is the NPC moving?
    private float moveTimeSeconds;      // Move time in seconds
    private float waitTimeSeconds;      // Wait time in seconds
    private Vector3 npcDirection;       // Direction the NPC is moving
    private Transform thisTransform;    // NPC's transform
    private Rigidbody2D thisRigidbody;  // NPC's rigidbody
    private Animator thisAnimator;      // NPC's animator

    private void Start()
    {
        // Set the transform, rigidbody, and animator components
        thisTransform = GetComponent<Transform>();
        thisRigidbody = GetComponent<Rigidbody2D>();
        thisAnimator = GetComponent<Animator>();

        // Create a random 
        moveTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
        waitTimeSeconds = Random.Range(minWaitTime, maxWaitTime);

        // Call switch direction
        SwitchDirection();
    }

    public override void Update()
    {
        // Do stuff in Sign.Update();
        base.Update();

        // This system keeps NPCs movement random
        if(isMoving)
        {
            // deltaTime is time interval in seconds from last frame to current frame
            moveTimeSeconds -= Time.deltaTime;

            if(moveTimeSeconds <= 0)
            {
                moveTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
                isMoving = false;
            }

            // If player is not in range of NPC, let NPC keep walking
            if (!playerInRange)
            {
                Walk();
            }
        }
        else
        {
            waitTimeSeconds -= Time.deltaTime;

            if(waitTimeSeconds <= 0)
            {
                // ChooseNewDirection helps keep movement random
                ChooseNewDirection();

                isMoving = true;

                waitTimeSeconds = Random.Range(minWaitTime, maxWaitTime);
            }
        }
    }

    private void ChooseNewDirection()
    {
        Vector3 t = npcDirection;
        int count = 0;

        SwitchDirection();

        while (t == npcDirection && count < 100)
        {
            SwitchDirection();

            count++;
        }
    }

    public void UpdateAnimation()
    {
        // Update the walking animation
        // ie: left, right, up, down
        thisAnimator.SetFloat("moveX", npcDirection.x);
        thisAnimator.SetFloat("moveY", npcDirection.y);
    }

    public void Walk()
    {
        // MUST USE Time.fixedDeltaTime!!!! Time.deltaTime DOES NOT WORK!!!!!!!!!
        // Because it's a physics based behavior, must used fixedDeltaTime
        Vector3 t = thisTransform.position + npcDirection * (walkSpeed * Time.fixedDeltaTime);

        // Make the NPC walk around!
        if (boundary.bounds.Contains(t))
        {
            thisRigidbody.MovePosition(t);
        }
        else
        {
            SwitchDirection();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If NPC collides with something
        ChooseNewDirection();
    }

    // Function that determines NPCs walking direction
    public void SwitchDirection()
    {
        // Random direction between 0 and 3
        int direction = Random.Range(0, 4);


        if(direction == 0)
        {
            npcDirection = Vector3.right;
        }
        else if(direction == 1)
        {
            npcDirection = Vector3.up;
        }
        else if(direction == 2)
        {
            npcDirection = Vector3.left;
        }
        else if(direction == 3)
        {
            npcDirection = Vector3.down;
        }
        else
        {
            npcDirection = Vector3.zero;
        }

        UpdateAnimation();
    }
}