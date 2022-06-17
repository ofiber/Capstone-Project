using UnityEngine;

/// <summary>
/// AreaLog defines a type of log enemy that will only chase the player if player is within the log's boundary.
/// The log will stop chasing the player if the player leaves the log's boundary.
/// </summary>
public class AreaLog : Log
{
    public Collider2D boundary;     // This enemy's movement boundary

    public override void FindDistance()
    {
        // If player is within the boundary and within the chase radius
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius && boundary.bounds.Contains(target.transform.position))
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Vector3 temp /*transform.position*/ = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                // Update the animation
                ChangeAnimation(temp - transform.position);

                // Move the enemy
                rb.MovePosition(temp);

                // Change enemy's state
                ChangeEnemyState(EnemyState.walk);

                // Change enemy's animation
                animator.SetBool("wakeUp", true);
            }
        }//         If player is outside of chase radius                             OR        If player is no longer inside boundary
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius || !boundary.bounds.Contains(target.transform.position))
        {
            // If player leaves chase radius, or leaves enemy's bounds
            // Put enemy to sleep :)
            animator.SetBool("wakeUp", false);
        }
    }
}
