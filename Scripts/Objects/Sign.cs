using UnityEngine;
using TMPro;

/// <summary>
/// Defines how an interactable sign works. Inherits from Interactable.
/// </summary>
public class Sign : Interactable
{
    [Header("Sign Settings")]
    public GameObject dialogBox;    // Dialog box to show
    public TMP_Text dialogText;     // Dialog text to show
    public string dialog;           // Dialog text


    public virtual void Update()
    {
        // If player presses 'F' key and is in range
        if (Input.GetKeyDown(KeyCode.F) && playerInRange)
        {
            // If dialog box is active
            if (dialogBox.activeInHierarchy)
            {
                // Deactivate it
                dialogBox.SetActive(false);
            }
            else
            {
                // Otherwise, active dialog box
                dialogBox.SetActive(true);

                // Set the dialog text
                dialogText.text = dialog;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        // If collider is player and not a trigger
        if (collider.CompareTag("Player") && !collider.isTrigger)
        {
            // Raise the signal
            context.RaiseSignal();

            // Set player in range false
            playerInRange = false;

            // Deactivate dialog box
            dialogBox.SetActive(false);
        }
    }
}