using UnityEngine;

/// <summary>
/// Log script defines enemy type
/// </summary>
public class Log : Enemy
{
    [Header("Locations")]
    public Transform target;        // Log's target, usually set to the player

    [Header("Object Attributes")]
    public Animator animator;       // Log's animator
    public Rigidbody2D rb;          // Log's rigidbody

    [Header("Log Attributes")]
    public float chaseRadius;       // Chase radius
    public float attackRadius;      // Attack radius

    private void Start()
    {
        // Set Log's current state to idle
        currentState = EnemyState.idle;

        // Get the rigidboy
        rb = GetComponent<Rigidbody2D>();

        // Get the animator
        animator = GetComponent<Animator>();

        // Set the target as the player
        target = GameObject.FindWithTag("Player").transform;

        // Wake up the log
        animator.SetBool("wakeUp", true);
    }

    private void FixedUpdate()
    {
        // Find the distance to the player
        FindDistance();
    }

    public virtual void FindDistance()
    {
        // If player is within chase radius, but not within attack radius
        if(Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Vector3 temp /*transform.position*/ = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                // Move the log
                ChangeAnimation(temp - transform.position);
                rb.MovePosition(temp);
                ChangeEnemyState(EnemyState.walk);
                animator.SetBool("wakeUp", true);
            }
        }
        else if(Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            // Otherwise put log to sleep :(
            animator.SetBool("wakeUp", false);
        }
    }

    /// <summary>
    /// Left = -x
    /// Right = x
    /// Up = y
    /// Down = -y
    /// </summary>
    public void ChangeAnimation(Vector2 direction)
    {
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if(direction.x > 0)
            {
                // If x is greater than zero, walking right
                SetAnimatorFloat(Vector2.right);
            }
            else if (direction.x < 0)
            {
                // If x is less than zero, walking left
                SetAnimatorFloat(Vector2.left);
            }
        }
        else if(Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if(direction.y > 0)
            {
                // If y is greater than zero, walking up
                SetAnimatorFloat(Vector2.up);
            }
            else if(direction.y < 0)
            {
                // If y is less than zero, walking down
                SetAnimatorFloat(Vector2.down);
            }
        }
    }

    public void SetAnimatorFloat(Vector2 v)
    {
        // Update log's animations
        animator.SetFloat("moveX", v.x);
        animator.SetFloat("moveY", v.y);
    }

    public void ChangeEnemyState(EnemyState newState)
    {
        // Updates log's state to newState if log is not already in that state
        if (currentState != newState)
        {
            currentState = newState;
        }
    }
}