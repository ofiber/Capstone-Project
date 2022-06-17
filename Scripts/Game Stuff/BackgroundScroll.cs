using UnityEngine;

/// <summary>
/// Controls the parallax effect that makes the background scroll in the main menu
/// </summary>
public class BackgroundScroll : MonoBehaviour
{
    [Header("Settings")]
    public float scrollSpeed = 0;   // Speed of the scroll

    private void Update()
    {
        // Get the renderer componenet and scrollllllll
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(Time.time * scrollSpeed, 0);
    }
}