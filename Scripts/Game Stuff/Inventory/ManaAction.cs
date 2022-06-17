using UnityEngine;

/// <summary>
/// DEPRECIATED
/// Was used on mana items that player's could pick up and put in
/// their inventories
/// </summary>
public class ManaAction : MonoBehaviour
{
    public FloatValue manaLevel;        // Mana level
    public Signal signal;               // Signal to raise

    public void Use(int increaseAmt)
    {
        // Increase mana by specified amount
        manaLevel.RuntimeValue += increaseAmt;

        // Raise the signal
        signal.RaiseSignal();
    }
}