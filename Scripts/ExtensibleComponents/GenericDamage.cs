using UnityEngine;

/// <summary>
/// DEPRECIATED
/// Dictates a generic damage system
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class GenericDamage : MonoBehaviour
{
    [SerializeField]private float damageAmt;        // Amount to damage entity
    [SerializeField]private string colliderTag;     // Tag for collider to check for

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // If the current collider has the specified tag and is a trigger
        if(collision.gameObject.CompareTag(colliderTag) && collision.isTrigger)
        {
            // Get the GenericHealth collider
            GenericHealth t = collision.GetComponent<GenericHealth>();

            if(t != null)
            {
                // Damage thing that collider is attached to
                t.Damage(damageAmt);
            }
        }
    }
}