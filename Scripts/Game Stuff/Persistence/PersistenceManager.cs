using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Manages the persistent state of specified objects
/// </summary>
public class PersistenceManager : MonoBehaviour
{
    // Singleton pattern
    // The variable 'instance' will persist between scenes
    [Header("Settings")]
    [Tooltip("This Manager's Instance")]public static PersistenceManager instance;
    [Tooltip("Objects To Save")]public List<ScriptableObject> objects = new List<ScriptableObject>();   // Lists better than arrays. Lists have dynamic sizing, and the .Contains() method

    private void OnEnable()
    {
        // Load the data
        LoadScriptableObjects();
    }

    private void Awake()
    {
        // Set instance if it is null
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            // Otherwise destroy this object
            Destroy(this.gameObject);
        }

        // Stop this object from being destroy when other scenes are loaded
        DontDestroyOnLoad(this);
    }

    public void SaveScriptableObjects()
    {
        // For all of the objects in the list of objects
        for (int i = 0; i < objects.Count; i++)
        {
            // Unity knows the default savegame path and uses persistentDataPath to store the path to that directory
            // Create and open the file
            FileStream saveFile = File.Create(Application.persistentDataPath + string.Format("/{0}.dat", i));

            // Create a new binary formatter
            BinaryFormatter bin = new BinaryFormatter();

            // Create a new json object from the object
            var json = JsonUtility.ToJson(objects[i]);

            // Serialize the json object to the file
            bin.Serialize(saveFile, json);

            // Close the file
            saveFile.Close();
        }
    }

    public void LoadScriptableObjects()
    {
        // For all of the obejcts in the list of objects
        for(int i = 0; i < objects.Count; i++)
        {
            // If the save file exists
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", i)))
            {
                // Open the save file
                FileStream saveFile = File.Open(Application.persistentDataPath + string.Format("/{0}.dat", i), FileMode.Open);

                // Create a new binary formatter
                BinaryFormatter bin = new BinaryFormatter();

                // Deserialize the data file and overwrite the temp object with it
                JsonUtility.FromJsonOverwrite((string)bin.Deserialize(saveFile), objects[i]);

                // Close the file
                saveFile.Close();
            }
        }
    }

    public void ResetScriptableObjects()
    {
        // For all of the objects in the list of objects
        for(int i = 0; i < objects.Count; i++)
        {
            // If the save file exists
            if(File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", i)))
            {
                // Delete the file
                File.Delete(Application.persistentDataPath + string.Format("/{0}.dat", i));
            }
        }
    }
}