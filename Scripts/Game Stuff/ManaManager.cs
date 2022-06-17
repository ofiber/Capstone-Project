using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the mana slider in the HUD
/// </summary>
public class ManaManager : MonoBehaviour
{
    public Slider manaSlider;       // Mana slider in HUD
    public Inventory inventory;     // Player's inventory

    // Start is called before the first frame update
    void Start()
    {
        // Set slider max value
        manaSlider.maxValue = inventory.maxMana;

        // Set slider current value to 70% of max value
        manaSlider.value = inventory.maxMana * 0.70f;

        // Set inventory mana level to 70% of inventory max mana level
        inventory.currentMana = inventory.maxMana * 0.70f;
    }

    public void AddMana()
    {
        // Sets the HUD slider value to player's current mana in the player's inventory
        manaSlider.value = inventory.currentMana;

        // If current slider value is more than max value
        if(manaSlider.value > manaSlider.maxValue)
        {
            // Set slider value to max value
            manaSlider.value = manaSlider.maxValue;

            // Set inventory value to max value
            inventory.currentMana = inventory.maxMana;
        }
    }

    public void RemoveMana()
    {
        // Sets the HUD slider value to player's current mana in the player's inventory
        manaSlider.value = inventory.currentMana;

        // If current slider value is less than zero
        if (manaSlider.value < 0)
        {
            // Set the value to zero
            manaSlider.value = 0;

            // Set the inventory value to zero
            inventory.currentMana = 0;
        }
    }
}