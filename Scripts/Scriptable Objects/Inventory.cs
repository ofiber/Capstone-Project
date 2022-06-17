using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the player's inventory in the background
/// </summary>
[CreateAssetMenu(menuName = "Inventory/Inventory", fileName = "NewInventory")]
[System.Serializable]
public class Inventory : ScriptableObject
{
    public Item currentItem;                        // Whatever current item is, ie: the item just grabbed from chest
    public List<Item> items = new List<Item>();     // Essentially the player's inventory
    public int numberOfKeys;                        // How many overworld keys the player has
    public int coins;                               // Number of coins player has collected
    public float maxMana = 10;                      // Player's max mana
    public float currentMana;                       // Player's current mana


    public void OnEnable()
    {
        // Set current mana to max mana
        currentMana = maxMana;
    }

    public void RemoveMana(float cost)
    {
        // Remove cost amount of mana from currentMana
        currentMana = currentMana - cost;
    }

    public void AddItem(Item itemToAdd)
    {
        // Is the item a key?
        if(itemToAdd.isKey)
        {
            // Increase number of keys
            numberOfKeys++;
        }
        else
        {
            // If item is not already in list of items
            if(!items.Contains(itemToAdd))
            {
                // Add item to list
                items.Add(itemToAdd);
            }
        }
    }


    public bool ItemCheck(Item item)
    {
        // If player inventory contains the item
        if (items.Contains(item))
        {
            // Return true
            return true;
        }
        else
        { 
            // Otherwise, return false
            return false;
        }
    }
}