using UnityEngine;

/// <summary>
/// Defines how knockback works
/// </summary>
public class KnockbackSystem : MonoBehaviour
{
    public float force;         // Amount of force to apply
    public float knockTime;     // Total time to apply knockback
    public float damage;        // Damage to deal

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the collider is breakable and this is playyer
        if (collision.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player"))
        {
            // Break the pot!!
            collision.GetComponent<Pot>().SmashPot();
        }

        // If the collider is enemy or player
        if (collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("Player"))
        {
            // Get the hit rigidbody
            Rigidbody2D hit = collision.GetComponent<Rigidbody2D>();

            // If hit exists
            if(hit != null)
            {
                // Get the position difference
                Vector2 diff = hit.transform.position - transform.position;
                
                // Normalize difference and apply force
                diff = diff.normalized * force;

                // Add force to the rigidbody
                hit.AddForce(diff, ForceMode2D.Impulse);

                // If the collider is enemy and trigger
                if (collision.gameObject.CompareTag("enemy") && collision.isTrigger)
                {
                    // Set the enemy's state
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;

                    // Call the Knock method from Enemy
                    collision.GetComponent<Enemy>().Knock(hit, knockTime, damage);
                }

                // If the collider is player
                if(collision.gameObject.CompareTag("Player"))
                {
                    // If the player is not already in the stagger state
                    if (collision.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)
                    {
                        // Set state to stagger
                        hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;

                        // Call Knock method from PlayerMovement
                        collision.GetComponent<PlayerMovement>().Knock(knockTime, damage);
                    }
                }
            }
        }
    }
}