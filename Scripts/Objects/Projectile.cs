using UnityEngine;

/// <summary>
/// Base class for projectiles
/// </summary>
public class Projectile : MonoBehaviour
{
    [Header("Projectile Attributes")]
    public float projLifetime;              // How long the projectile stays in the game after being fired
    public float projMoveSpeed;             // How fast the projectile should move
    public Vector2 directionToTravel;       // What direction the projectile should move in

    [Header("Projetile Collisions")]
    public Rigidbody2D myRigidbody;         // Projectile rigidbody

    private float lifetimeSeconds;          // Private version of projLifetime
    

    private void Start()
    {
        // Set the rigidbody
        myRigidbody = GetComponent<Rigidbody2D>();

        // Set lifetimeSeconds
        lifetimeSeconds = projLifetime;
    }

    private void Update()
    {
        // Decrease lifetime
        lifetimeSeconds -= Time.deltaTime;

        // If lifetime is below zero -> prevents memory leaks
        if (lifetimeSeconds <= 0)
        {
            // Destroy the projecttile
            Destroy(this.gameObject);
        }
    }

    public void Launch(Vector2 initVelocity)
    {
        // Move the projectile
        myRigidbody.velocity = initVelocity * projMoveSpeed;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // If the projectile collides with something

        // Destroy it
        Destroy(this.gameObject);
    }
}