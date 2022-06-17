using UnityEngine;

/// <summary>
/// DEPRECIATED
/// Dictates a generic health system
/// </summary>
public class GenericHealth : MonoBehaviour
{
    public FloatValue maxHealth;    // Entity's max health
    public float currHealth;        // Entity's current health

    private void Start()
    {
        // Set current health to max health
        currHealth = maxHealth.RuntimeValue;
    }

    /// <summary>
    /// Heal by healAmt, and ensures current health does not exceed max health
    /// </summary>
    /// <param name="healAmt"></param>
    public virtual void Heal(float healAmt)
    {
        currHealth += healAmt;

        if(currHealth < maxHealth.RuntimeValue)
        {
            currHealth = maxHealth.RuntimeValue;
        }
    }

    /// <summary>
    /// Fully heal, set current health equal to max health
    /// </summary>
    public virtual void FullHeal()
    {
        currHealth = maxHealth.RuntimeValue;
    }

    /// <summary>
    /// Damage by damageAmt, and ensure current health does not drop below zero
    /// </summary>
    /// <param name="damageAmt"></param>
    public virtual void Damage(float damageAmt)
    {
        currHealth -= damageAmt;

        if(currHealth < 0)
        {
            currHealth = 0;
        }
    }

    /// <summary>
    /// InstaKill, sets current health to zero
    /// </summary>
    public virtual void InstantKill()
    {
        currHealth = 0;
    }
}