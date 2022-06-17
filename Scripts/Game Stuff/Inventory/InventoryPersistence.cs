using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// DEPRECIATED
/// Managed the persistence of the player's inventory
/// </summary>
public class InventoryPersistence : MonoBehaviour
{
    [SerializeField] private PlayerInventory inventory;     // Player's inventory

    private void OnEnable()
    {
        // Clear inventory so we can load to empty inventory
        inventory.inventoryItems.Clear();

        // Load the data
        LoadScriptableObjects();
    }

    private void OnDisable()
    {
        // Save data
        SaveScriptableObjects();
    }

    public void SaveScriptableObjects()
    {
        // Reset the scriptable objects
        ResetScriptableObjects();

        // For all of the items in the player's inventory
        for (int i = 0; i < inventory.inventoryItems.Count; i++)
        {
            // Unity knows the default savegame path and uses persistentDataPath to store the path to that directory
            // Create and open the file
            FileStream saveFile = File.Create(Application.persistentDataPath + string.Format("/{0}.inv", i));

            // Create a new binary formatter
            BinaryFormatter bin = new BinaryFormatter();

            // Create a new json object from inventory item
            var json = JsonUtility.ToJson(inventory.inventoryItems[i]);

            // Serialize the json object to the file
            bin.Serialize(saveFile, json);

            // Close the file
            saveFile.Close();
        }
    }

    public void LoadScriptableObjects()
    {
        int i = 0;

        // While there are files in the save folder
        while (File.Exists(Application.persistentDataPath + string.Format("/{0}.inv", i)))
        {
            // Create a temp object
            var temp = ScriptableObject.CreateInstance<InventoryItem>();

            // Open the save file
            FileStream saveFile = File.Open(Application.persistentDataPath + string.Format("/{0}.inv", i), FileMode.Open);

            // Create a new binary formatter
            BinaryFormatter bin = new BinaryFormatter();

            // Deserialize the data file and overwrite the temp object with it
            JsonUtility.FromJsonOverwrite((string)bin.Deserialize(saveFile), temp);

            // Add temp to inventory
            inventory.inventoryItems.Add(temp);

            // Close the file
            saveFile.Close();

            i++;
        }
    }

    public void ResetScriptableObjects()
    {
        int i = 0;

        // While files exist in the folder
        while (File.Exists(Application.persistentDataPath + string.Format("/{0}.inv", i)))
        {
            // Delete the files
            File.Delete(Application.persistentDataPath + string.Format("/{0}.inv", i));

            i++;
        }
    }
}
