using UnityEngine;

/// <summary>
/// Defines the health potion power up
/// </summary>
public class Heart : PowerUp
{
    [Header("Potion Power Up Settings")]
    public FloatValue playerHealth;         // Player health
    public FloatValue heartContainers;      // How many heart containers the player should have currently
    public float healthIncreaseAmount;      // How much player's health will increase. Remember: 2 = 1 full heart, 1 = half a heart

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // If the thing that entered the trigger area is the player AND NOT the trigger
        if(collision.CompareTag("Player") && !collision.isTrigger)
        {
            // Increases player health by specified amount
            playerHealth.RuntimeValue += healthIncreaseAmount;

            // If health is more than it should be
            if(playerHealth.RuntimeValue > heartContainers.RuntimeValue * 2f)
            {
                // Set player health to max health
                playerHealth.RuntimeValue = heartContainers.RuntimeValue * 2f;
            }

            // Tells UI to check to see how many hearts should be displayed in health bar
            powerUpSignal.RaiseSignal();

            // Destroy the object
            Destroy(this.gameObject);
        }
    }
}