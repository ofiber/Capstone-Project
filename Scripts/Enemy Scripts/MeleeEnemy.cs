using System.Collections;
using UnityEngine;

/// <summary>
/// Enemy type that attacks player with a sword
/// </summary>
public class MeleeEnemy : Log
{
    [Header("Spawn Control")]
    public BooleanValue spawnControl;


    public void OnEnable()
    {
        if(spawnControl != null)
        {
            if(spawnControl.RuntimeValue)
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    public override void Awake()
    {
        base.Awake();

        if (spawnControl != null)
        {
            spawnControl.RuntimeValue = spawnControl.initVal;
        }
    }


    public override void FindDistance()
    {
        // If player is within chase radius, but not within attack radius
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Vector3 temp /*transform.position*/ = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                // Move the enemy
                ChangeAnimation(temp - transform.position);
                rb.MovePosition(temp);
                ChangeEnemyState(EnemyState.walk);
            }
        }
        else if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) <= attackRadius)
        {
            if (currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                // If player is in range of enemy, start attack coroutine
                StartCoroutine(AttackCoroutine());
            }
        }
    }

    public IEnumerator AttackCoroutine()
    {
        // Set state to attack
        currentState = EnemyState.attack;

        // Set animator attack state
        animator.SetBool("attack", true);

        // Wait for time
        yield return new WaitForSeconds(1f);

        // Set state to attack
        currentState = EnemyState.walk;

        // Reset animator attack state
        animator.SetBool("attack", false);
    }
}