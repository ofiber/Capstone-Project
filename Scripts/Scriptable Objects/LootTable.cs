using UnityEngine;

/// <summary>
/// Defines a loot table
/// </summary>
[System.Serializable]
public class Loot
{
    [Header("Loot Settings")]
    public PowerUp thisLoot;        // The loot being looted
    public int lootChance;          // Chance of this loot getting dropped
}


/// <summary>
/// Defines how a loot table generates loot
/// </summary>
[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    [Header("Loot Table Settings")]
    public Loot[] loot;     // List of loot that this table could generate

    /// <summary>
    /// Cumulative probability loot table
    /// </summary>
    /// <returns>Loot or NULL</returns>
    public PowerUp LootPowerUp()
    {
        // Cumulative probability
        int cumulativeProb = 0;

        // Current probability
        int currentProb = Random.Range(0, 100);

        // For all items in list of possible loot
        for(int i = 0; i < loot.Length; i++)
        {
            // Increase by loot's chance of being dropped
            cumulativeProb += loot[i].lootChance;

            // If current probability is lower than cumulative
            if(currentProb <= cumulativeProb)
            {
                // Return the loot
                return loot[i].thisLoot;
            }
        }

        // Otherwise, return null
        return null;
    }
}