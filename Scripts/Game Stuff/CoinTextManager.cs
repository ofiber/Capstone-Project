using UnityEngine;
using TMPro;


/// <summary>
/// Manages the text of the coin display sprite
/// </summary>
public class CoinTextManager : MonoBehaviour
{
    public Inventory playerInventory;   // Player's inventory
    public TMP_Text coinText;           // Text that displays how many coins player has

    public void UpdateCoinCount()
    {
        // Change coin text to the amount of coins in players inventory
        coinText.text = "" + playerInventory.coins;
    }
}