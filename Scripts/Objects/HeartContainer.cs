using UnityEngine;

/// <summary>
/// DEPRECIATED
/// Defines a heart container object. Increases player's max health on pickup
/// </summary>
public class HeartContainer : PowerUp
{
    [Header("Heart Container Settings")]
    public FloatValue numHeartContainers;   // Number of heart containers
    public FloatValue currentHealth;        // Players current health

    private float maxValue = 5;             // Players max health

    private void OnEnable()
    {
        // If player's health is greater than or equal to the max value
        if(numHeartContainers.RuntimeValue >= maxValue)
        {
            // Destroy the object
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // If player collides with object
        if(collision.gameObject.CompareTag("Player"))
        {
            // If player picks up a heart container object,
            // Add a heart container
            numHeartContainers.RuntimeValue = numHeartContainers.RuntimeValue + 1;

            // If player's health is greater than or equal to max value
            if(numHeartContainers.RuntimeValue > maxValue)
            {
                // Set current value to max value
                numHeartContainers.RuntimeValue = maxValue;
            }

            // Refill players health
            // Multiply by 2 because a value of 1 in currentHealth is half a heart
            currentHealth.RuntimeValue = numHeartContainers.RuntimeValue * 2;

            // Raise the signal
            powerUpSignal.RaiseSignal();

            // Destroy the game object
            Destroy(this.gameObject);
        }
    }
}