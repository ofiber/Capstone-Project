using UnityEngine;

/// <summary>
/// DEPRECIATED
/// Defines a mega heart powerup that fully heals player
/// </summary>
public class MegaHeart : Heart
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        // If collision tag is Player and not a trigger
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            // Increases player health by specified amount
            playerHealth.RuntimeValue = playerHealth.initVal;

            // Tells UI to check to see how many hearts should be displayed in health bar
            powerUpSignal.RaiseSignal();

            // Destroy the object
            Destroy(this.gameObject);
        }
    }
}