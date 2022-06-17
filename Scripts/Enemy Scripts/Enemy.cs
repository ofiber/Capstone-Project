using System.Collections;
using UnityEngine;

/// <summary>
/// Defines an enemy's possible states
/// </summary>
public enum EnemyState
{
    idle,       // Enemy is not moving (aka sleepin)
    walk,       // Enemy is walking
    attack,     // Enemy is attacking something
    stagger     // Enemy has been hit
}

/// <summary>
/// Base class for enemies. All enemies will inherit from this class
/// </summary>
public class Enemy : MonoBehaviour
{
    [Header("State  Machine")]
    public EnemyState currentState;         // Current state enemy is in

    [Header("Enemy Attributes")]
    public FloatValue maxHealth;            // Enemies' maximum health
    public float health;                    // Enemies' current health
    public string enemyName;                // Enemies' name
    public int baseAttack;                  // Enemies' base attack damage
    public float moveSpeed;                 // Enemies' movement speed
    public Vector2 homePosition;            // Enemies home(default) position

    [Header("Death Settings")]
    public GameObject deathEffect;          // Death effect to be played when enemy dies
    private float deathEffectDelay = 1f;    // Delay of 1, equivalent to 1 second
    public LootTable thisEnemiesLoot;       // Loot that this enemy should drop

    [Header("Signals")]
    public Signal roomSignal;


    public virtual void Awake()
    {
        // Set enemy's health on awake
        health = maxHealth.initVal;
    }

    private void OnEnable()
    {
        // Move the enemy back to it's home position
        transform.position = homePosition;

        // Reset enemy's health
        health = maxHealth.initVal;

        // Set enemy's state to idle
        currentState = EnemyState.idle;
    }

    private void DropLoot()
    {
        // If a loot table exists
        if(thisEnemiesLoot != null)
        {
            PowerUp current = thisEnemiesLoot.LootPowerUp();

            // If loot table generated loot
            if(current != null)
            {
                // Instantiate the loot into game world
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }

    private void DeathEffect()
    {
        // If this enemy requires a death effect, this will be true
        if(deathEffect != null)
        {
            // Create a deathEffect game object at the enemies' current position
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);

            // Destroy the newly created object after 1 second
            Destroy(effect, deathEffectDelay);
        }
    }

    public void Knock(Rigidbody2D e, float kT, float damage)
    {
        // Start the knockback coroutine
        StartCoroutine(KnockbackRoutine(e, kT));

        // Make the enemy take damage
        TakeDamage(damage);
    }

    private void TakeDamage(float damage)
    {
        // Apply damage amount to enemy's health
        health -= damage;

        // If health is zero or less, enemy is dead :(
        if(health <= 0)
        {
            // Play the death animation
            DeathEffect();

            // Drop loot
            DropLoot();

            // Raise the room signal
            if(roomSignal != null)
            {
                roomSignal.RaiseSignal();
            }
            
            // Stop showing enemy in scene
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator KnockbackRoutine(Rigidbody2D e, float knockTime)
    {
        if (e != null)
        {
            // Wait for knock time seconds
            yield return new WaitForSeconds(knockTime);

            e.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            e.velocity = Vector2.zero;
        }
    }
}