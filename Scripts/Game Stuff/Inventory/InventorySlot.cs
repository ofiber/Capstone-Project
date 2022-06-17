using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// DEPRECIATED
/// Defines an object that would be added to the inventory screen to display items
/// </summary>
public class InventorySlot : MonoBehaviour
{
    [Header("Inventory Slot UI")]
    [SerializeField] private TextMeshProUGUI itemAmountText;    // How many items the player has
    [SerializeField] private Image itemImage;                   // Sprite of the item

    [Header("Inventory Slot Attributes")]
    public InventoryItem thisItem;                              // Current item
    public InventoryManager manager;                            // The manager of the inventory

    public void SlotSetup(InventoryItem item, InventoryManager newManager)
    {
        // Set this item to the current item
        thisItem = item;

        // Set the inventory manager
        manager = newManager;

        // If the item exists
        if(thisItem != null)
        {
            // Set the item sprite
            itemImage.sprite = thisItem.itemSprite;

            // Set the amount owned text
            itemAmountText.text = thisItem.numOwned.ToString();
        }
    }

    public void OnClick()
    {
        // If the item exists
        if(thisItem != null)
        {
            // Setup the item's description and use button (<- if item is usable)
            manager.SetupDescAndButton(thisItem.itemDesc, thisItem.isUsable, thisItem);
        }
    }
}