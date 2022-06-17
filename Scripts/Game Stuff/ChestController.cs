using UnityEngine;

/// <summary>
/// DEPRECIATED -> Fixed with the persistence system
/// This script disabled chest if players already had the item inside
/// </summary>
public class ChestController : MonoBehaviour
{
    [Header("Settings")]
    public Item item;           // Item in the chest
    public Inventory inv;       // Player's inventory
    public GameObject chest;    // Chest!

    private void Start()
    {
        // If the player's inventory contains the item
        if(inv.ItemCheck(item))
        {
            // Deactivate chest
            chest.SetActive(false);
        }
    }
}