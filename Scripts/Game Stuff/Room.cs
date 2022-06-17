using UnityEngine;

/// <summary>
/// Manages different game 'rooms' in the scene
/// </summary>
public class Room : MonoBehaviour
{
    [Header("Lists")]
    public Enemy[] enemies;     // Enemies in this room
    public Pot[] pots;          // List of pots in this room

    [Header("Virtual Cameras")]
    public GameObject vCam;     // Cinemachine virtual camera

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // If the collider is player and not trigger
        if(collision.CompareTag("Player") && !collision.isTrigger)
        {
            // Activate all enemies
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], true);
            }

            // Activate all pots
            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], true);
            }

            // Activate the cinemachine virtual camera
            vCam.SetActive(true);
        }
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        // If the collider is player and not trigger
        if(collision.CompareTag("Player") && !collision.isTrigger)
        {
            // Deactivate all enemies
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], false);
            }

            // Deactivate all pots
            for ( int i = 0;i < pots.Length; i++)
            {
                ChangeActivation(pots[i], false);
            }
            
            // Deactivate the cinemachine virtual camera
            vCam.SetActive(false);
        }
    }

    public void ChangeActivation(Component component, bool state)
    {
        // Change the activation state of the component
        component.gameObject.SetActive(state);
    }
}