using UnityEngine;

/// <summary>
/// Log enemy type that patrols between two or more points
/// </summary>
public class PatrolLog : Log
{
    [Header("Patrol Log Settings")]
    public float roundingDistance;      // The rounding distance

    [Header("Patrol Points")]
    public Transform[] path;            // List of points that this enemy patrols along
    public Transform currentGoal;       // Next point for enemy to patrol towards
    public int currentPoint;            // Current point that enemy is at

    public override void FindDistance()
    {
        // If player is within chase radius, but not within attack radius
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Vector3 temp /*transform.position*/ = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                // Move the log
                ChangeAnimation(temp - transform.position);
                rb.MovePosition(temp);
                animator.SetBool("wakeUp", true);
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            // If enemy is not chasing player

            // If patrol log is farther away from goal than rounding distance, move towards goal
            if (Vector3.Distance(transform.position, path[currentPoint].position) > roundingDistance)
            {
                Vector3 temp /*transform.position*/ = Vector3.MoveTowards(transform.position, path[currentPoint].position, moveSpeed * Time.deltaTime);

                // Move towards next point
                ChangeAnimation(temp - transform.position);
                rb.MovePosition(temp);
            }
            else
            {
                // Else, change goal
                ChangeGoal();
            }
        }
    }

    /// <summary>
    /// Next point to move to logic
    /// </summary>
    private void ChangeGoal()
    {
        // If current goal is eqaul to size of array, reset goal to first point in array
        if(currentPoint == path.Length - 1)
        {
            currentPoint = 0;
            currentGoal = path[0];
        }
        else
        {
            // Else, update goal to next point in array
            currentPoint++;
            currentGoal = path[currentPoint];
        }
    }
}