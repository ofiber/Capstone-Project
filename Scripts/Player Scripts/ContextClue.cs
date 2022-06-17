using UnityEngine;

/// <summary>
/// Defines a context clue that displays when player is in range of an interactable object
/// </summary>
public class ContextClue : MonoBehaviour
{
    [Header("Context Clue Settings")]
    public GameObject contextClue;          // Context clue object
    public bool contextActive = false;      // Is context active

    public void ChangeContext()
    {
        // Change contextActive to opposite
        contextActive = !contextActive;

        // If context is active
        if(contextActive)
        {
            // Activate context clue
            contextClue.SetActive(true);
        }
        else
        {
            // Otherwise, deactivate context clue
            contextClue.SetActive(false);
        }
    }
}