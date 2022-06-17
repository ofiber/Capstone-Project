using UnityEngine;

/// <summary>
/// DEPRECIATED
/// Defines an item that the player could pick up and add to their inventory
/// </summary>
public class PhysicalInventoryItem : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private PlayerInventory inventory;     // Player's inventory
    [SerializeField] private InventoryItem thisItem;        // The current item

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player 'picks up' the object
        if(collision.gameObject.CompareTag("Player") && collision.isTrigger)
        {
            // Add the item to the players inventory
            AddItem();

            // Destroy the game object
            Destroy(this.gameObject);
        }
    }

    public void AddItem()
    {
        // If the inventory exists and the item exists
        if(inventory && thisItem)
        {
            // If we pick up a new Item and already have one or more in our inventory
            // increase count of that item in inventory
            if(inventory.inventoryItems.Contains(thisItem))
            {
                thisItem.numOwned++;
            }
            else
            {
                // Else, add the item to inventory
                inventory.inventoryItems.Add(thisItem);

                // And increase the amount owned
                thisItem.numOwned++;
            }
        }
    }

}