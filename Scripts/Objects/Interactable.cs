using UnityEngine;

/// <summary>
/// Base class for interactable objects like signs, doors, treasure chests, etc.
/// </summary>
public class Interactable : MonoBehaviour
{
    [Header("Interactable Settings")]
    public Signal context;              // Signal
    public string collisionTag = "";    // Tag to compare collision to, ie: Player
    public bool playerInRange;          // If player is range of the interactable

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // If collider tag is collisionTag and is not a trigger
        if (collider.CompareTag(collisionTag) && !collider.isTrigger)
        {
            // Raise signal
            context.RaiseSignal();

            // Set player in range true
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        // If collider tag is collisionTage and is not trigger
        if (collider.CompareTag(collisionTag) && !collider.isTrigger)
        {
            // Raise signal
            context.RaiseSignal();

            // Set player in range false
            playerInRange = false;
        }
    }
}