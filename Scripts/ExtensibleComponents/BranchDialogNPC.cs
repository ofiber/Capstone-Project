using UnityEngine;

/// <summary>
/// Extensible script for an NPC with branching dialog 
/// </summary>
public class BranchDialogNPC : Interactable
{
    [Header("Branching Dialog NPC Settings")]
    public TextAssetValue textAsset;    // Middleman dialog
    public TextAsset dialogAsset;       // NPCs dialog asset
    public Signal canvasSignal;         // Signal to turn on/off dialog canvas

    private void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                // If player is in range an pressed the 'F' key

                // Assign the dialog value
                textAsset.value = dialogAsset;

                // Raise the signal to turn on the branch dialog canvas
                canvasSignal.RaiseSignal();
            }
        }
    }
}
