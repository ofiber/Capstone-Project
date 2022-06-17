using UnityEngine;

/// <summary>
///  Defines an arrow projectile that is shot from a bow
/// </summary>
public class Arrow : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed;     // Movement speed of the arrow
    public float arrowLife;     // How long the arrow will stay in the world if it doesn't hit an enemy
    public float arrowCost;     // Mana cost of the arrow

    [Header("Components")]
    public Rigidbody2D rBody;   // Arrow's rigidbody componenent

    // Privates
    private float lifeCount;    // How long the arrow will stay in the world before being destroyed, if it hasn't hit anything

    private void Start()
    {
        // Set life count to arrow life
        lifeCount = arrowLife;
    }

    private void Update()
    {
        // Using Time.deltaTime because deltaTime is independent of framerate,
        // so time will be consistent across all systems
        lifeCount = lifeCount - Time.deltaTime;

        // If life has run out
        if(lifeCount <= 0)
        {
            // Destroy the arrow
            Destroy(this.gameObject);
        }
    }

    public void Setup(Vector2 velocity, Vector3 direction)
    {
        // Normalize velocity otherwise it will move faster diagonally than it does vertically/horizontally
        rBody.velocity = velocity.normalized * moveSpeed;

        // Converts direction to an Euler angle using quaternion rotation
        transform.rotation = Quaternion.Euler(direction);
    }

    // Ensures arrow object gets destroyed when it hits an enemy to prevent memory leak
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // If the arrow hits an enemy
        if(collision.gameObject.CompareTag("enemy"))
        {
            // Destroy the arrow
            Destroy(this.gameObject);
        }
    }
}