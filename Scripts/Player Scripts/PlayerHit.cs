using UnityEngine;

/// <summary>
/// When player hits a pot
/// </summary>
public class PlayerHit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // If collision is breakable
        if(collider.CompareTag("breakable"))
        {
            // Smash the pot!!
            collider.GetComponent<Pot>().SmashPot();
        }
    }
}