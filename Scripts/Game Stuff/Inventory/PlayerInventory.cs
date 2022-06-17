using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// DEPRECIATED
/// Literally just a list of InventoryItems
/// </summary>
[System.Serializable]
[CreateAssetMenu(fileName = "NewInventory", menuName = "Inventory/Player Inventory")]
public class PlayerInventory : ScriptableObject
{
    [Header("Player Inventory Items")]
    public List<InventoryItem> inventoryItems = new List<InventoryItem>();      // List of InventoryItems
}