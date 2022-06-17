using UnityEngine;

/// <summary>
/// DEPRECIATED
/// Was used on health items that player's could pick up and put in
/// their inventories
/// </summary>
public class HealthAction : MonoBehaviour
{
    public FloatValue healthLevel;      // Health level
    public Signal signal;               // Signal to raise

    public void Use(int increaseAmt)
    {
        // Increase health by specified amount
        healthLevel.RuntimeValue += increaseAmt;

        // Raise the signal
        signal.RaiseSignal();
    }
}