using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages players heart display
/// </summary>
public class HeartManager : MonoBehaviour
{
    public Image[] hearts;                      // Array of heart images
    public Sprite fullHeart;                    // Full heart sprite
    public Sprite halfHeart;                    // Half heart sprite
    public Sprite emptyHeart;                   // Empty heart sprite
    public FloatValue heartContainers;          // Number of heart containers player has
    public FloatValue playerCurrentHealth;      // Player's health

    private void Start()
    {
        // Initialize hearts
        InitHearts();

        // Ensures that when a game is loaded after death, players health resets
        if(playerCurrentHealth.RuntimeValue <= 0)
        {
            playerCurrentHealth.RuntimeValue = heartContainers.RuntimeValue * 2;
        }
    }

    public void InitHearts()
    {
        // For all heart containers player has
        for(int i = 0; i < heartContainers.RuntimeValue; i++)
        {
            if (i < hearts.Length)
            {
                // Activate object
                hearts[i].gameObject.SetActive(true);

                // Set sprite
                hearts[i].sprite = fullHeart;
            }
        }
    }

    public void UpdateHearts()
    {
        // Call to init in case player has picked up more hearts since last update
        InitHearts();

        // Divide by two because half a heart == 1 health point
        // ex: 5 health points == 2.5 hearts
        float tempHealth = playerCurrentHealth.RuntimeValue / 2;

        // For all heart containers player has
        for(int i = 0; i < heartContainers.RuntimeValue; i++)
        {
            if(i <= tempHealth - 1)
            {
                // Full heart
                hearts[i].sprite = fullHeart;
            }
            else if(i >= tempHealth)
            {
                // Empty heart
                hearts[i].sprite = emptyHeart;
            }
            else
            {
                // Half heart
                hearts[i].sprite = halfHeart;
            }
        }
    }
}