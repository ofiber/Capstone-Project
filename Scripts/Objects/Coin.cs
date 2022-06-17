using UnityEngine;

/// <summary>
/// Defines the coin object that players can pick up
/// </summary>
public class Coin : PowerUp
{
    public Inventory playerInventory;   // Player's inventory

    private void Start()
    {
        // Raise the signal
        powerUpSignal.RaiseSignal();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // If the thing that entered the trigger area is the player AND NOT the trigger
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            // Add a coin to the players inventory
            playerInventory.coins += 1;

            // Tells UI to check to see how many hearts should be displayed in health bar
            powerUpSignal.RaiseSignal();

            // Destroy the game object, simulates player "picking up" the power up
            Destroy(this.gameObject);
        }
    }
}