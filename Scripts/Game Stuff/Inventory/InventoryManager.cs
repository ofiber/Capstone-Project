using UnityEngine;
using TMPro;

/// <summary>
/// DEPRECIATED
/// Manages the inventory menu
/// </summary>
public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Manager Settings")]
    [Tooltip("PlayerInventory Scriptable Object")]public PlayerInventory playerInventory;
    public InventoryItem currentInventoryItem;
    [SerializeField, Tooltip("Inventory Slot Prefab")] private GameObject inventorySlot;
    [SerializeField, Tooltip("ScrollView Componenet")] private GameObject inventoryScrollView;
    [SerializeField, Tooltip("DesctriptionLabel text field")] private TextMeshProUGUI itemDescText;
    [SerializeField, Tooltip("DesctriptionLabel use button")] private GameObject useItemButton;

    private void OnEnable()
    {
        // On enable, clear inventory
        ClearInventory();

        // Remake inventory slots
        MakeInventorySlot();

        // Set attributes
        SetupAttributes("", false);
    }

    public void SetupDescAndButton(string desc, bool isUseable, InventoryItem currentItem)
    {
        // Set current inventory item
        currentInventoryItem = currentItem;

        // Set item description
        itemDescText.text = desc;

        // Enable the use button
        useItemButton.SetActive(isUseable);
    }

    public void MakeInventorySlot()
    {
        // If the player inventory exists
        if(playerInventory != null)
        {
            // Loop through the list items in player's inventory
            for(int i = 0; i < playerInventory.inventoryItems.Count; i++)
            {
                if (playerInventory.inventoryItems[i].numOwned > 0)
                {
                    // Instantiate a temporary inventory slot item
                    GameObject t = Instantiate(inventorySlot, inventoryScrollView.transform.position, Quaternion.identity);

                    // Set the scroll view as the temp slot's parent
                    t.transform.SetParent(inventoryScrollView.transform);

                    InventorySlot newSlot = t.GetComponent<InventorySlot>();

                    if (newSlot != null)
                    {
                        // Setup the slot with the i'th item in playerinventory, and set the manager to this script
                        newSlot.SlotSetup(playerInventory.inventoryItems[i], this);
                    }
                }
            }
        }
    }

    public void SetupAttributes(string desc, bool buttonIsActive)
    {
        // Set item description
        itemDescText.text = desc;

        // If item is a useable item
        if(buttonIsActive)
        {
            // Enable use button
            useItemButton.SetActive(true);
        }
        else
        {
            // Otherwise, disable use button
            useItemButton.SetActive(false);
        }
    }

    public void ClearInventory()
    {
        // Loop through all items in the current scroll area and delete them
        for(int i = 0; i < inventoryScrollView.transform.childCount; i++)
        {
            Destroy(inventoryScrollView.transform.GetChild(i).gameObject);
        }
    }

    public void OnClickUseButton()
    {
        if(currentInventoryItem != null)
        {
            // Use the item
            currentInventoryItem.Use();

            // Clear inventory screen
            ClearInventory();

            // Reload inventory screen to update usage numbers
            MakeInventorySlot();

            if(currentInventoryItem.numOwned == 0)
            {
                SetupAttributes("", false);
            }
        }
    }
}