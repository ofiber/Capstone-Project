using System.Collections;
using UnityEngine;

/// <summary>
/// Defines the breakable pot world object
/// </summary>
public class Pot : MonoBehaviour
{
    [Header("Loot Settings")]
    public LootTable thisPotsLoot;  // The pot's loot table

    private Animator animator;      // The pot's animator

    private void Start()
    {
        // Set the animator
        animator = GetComponent<Animator>();
    }

    public void SmashPot()
    {
        // Play the smash animation
        animator.SetBool("smash", true);

        // Start the coroutine
        StartCoroutine(BreakRoutine());
    }

    IEnumerator BreakRoutine()
    {
        // Wait for a 1/3 of a second
        yield return new WaitForSeconds(0.3f);

        // Deactivate the pot
        this.gameObject.SetActive(false);

        // Drop loot
        DropLoot();
    }
    private void DropLoot()
    {
        // If a loot table exists
        if (thisPotsLoot != null)
        {
            // Create a new power up
            PowerUp current = thisPotsLoot.LootPowerUp();

            // If loot table generated loot
            if (current != null)
            {
                // Instantiate the loot into game world
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }
}