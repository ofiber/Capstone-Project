using UnityEngine;

/// <summary>
/// Defines the mana bottle power up
/// </summary>
public class ManaBottle : PowerUp
{
    [Header("Mana Bottle Settings")]
    public Inventory inventory;     // Player's inventory
    public float manaAmount;        // Amount to increase mana by

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // If player collides with this object
        if(collision.gameObject.CompareTag("Player"))
        {
            // Increase inventory's current mana by manaAmount
            inventory.currentMana = inventory.currentMana + manaAmount;

            if(inventory.currentMana > inventory.maxMana)
            {
                inventory.currentMana = inventory.maxMana;
            }

            // Raise signal
            powerUpSignal.RaiseSignal();

            // Destroy the object
            Destroy(this.gameObject);
        }
    }
}