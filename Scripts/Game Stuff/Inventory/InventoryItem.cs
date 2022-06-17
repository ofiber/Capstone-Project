using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// DEPRECIATED
/// Dictates an item that could be added to the player's inventory
/// </summary>
[System.Serializable]
[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    [Header("Inventory Item Settings")]
    public UnityEvent thisEvent;        // Event?
    public Sprite itemSprite;       // Sprite of the item
    public string itemName;         // Name of the item
    public string itemDesc;         // Description of the item
    public int numOwned;            // How many of these items the player has
    public bool isUsable;           // Is the item a useable item
    public bool isUnique;           // Is the item unique

    public void Use()
    {
        // If item is useable, invoke the event
        thisEvent.Invoke();
    }

    public void DecreaseNumOwned(int numToDecrease)
    {
        // Decrease the amount of items currently owned
        numOwned -= numToDecrease;

        // If the amount currently owned falls below zero,
        // reset it to zero
        if(numOwned < 0)
        {
            numOwned = 0;
        }
    }
}