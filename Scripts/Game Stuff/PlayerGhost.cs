using UnityEngine;

/// <summary>
/// This script is stupid...
/// Makes the main menu character animated :)
/// </summary>
public class PlayerGhost : MonoBehaviour
{
    Animation thisAnimation;            // This animation
    public GameObject playerGhost;      // Player to animate

    void Awake()
    {
        // Get the animation
        thisAnimation = playerGhost.GetComponent<Animation>();

        // Play the animation
        thisAnimation.Play("PlayerGhost");
    }
}