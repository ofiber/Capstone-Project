using UnityEngine;

/// <summary>
/// Turret log enemy type that shoots rocks at player
/// </summary>
public class TurretLog : Log
{
    [Header("Turret Log Ammo")]
    public GameObject rockProjectile;   // Projectile to shoot
    public float fireDelay;             // Deley between firing
    public bool canFire = true;         // Can the log fire?

    private float fireDelaySeconds;     // Fire delay in seconds

    private void Update()
    {
        // Update fire delay
        fireDelaySeconds -= Time.deltaTime;

        // If fireDelaySeconds is zero or lower, turret can fire
        if(fireDelaySeconds <= 0)
        {
            canFire = true;
            fireDelaySeconds = fireDelay;
        }
    }

    public override void FindDistance()
    {
        // If player is within chase radius, but not within attack radius
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                // The turret log cannot move

                if (canFire)
                {
                    // Distance between turret log and player (target is always set to player)
                    Vector3 temp = target.transform.position - transform.position;

                    // Instantiate projectile into game world
                    GameObject currentProj = Instantiate(rockProjectile, transform.position, Quaternion.identity);

                    // Launch projecttile towards player
                    currentProj.GetComponent<Projectile>().Launch(temp);

                    canFire = false;

                    ChangeEnemyState(EnemyState.walk);
                    animator.SetBool("wakeUp", true);
                }
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            // Put log to sleep :(
            animator.SetBool("wakeUp", false);
        }
    }
}