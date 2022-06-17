using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles transfer of player between scenes
/// </summary>
public class SceneTransition : MonoBehaviour
{
    [Header("New Scene Settings")]
    public string sceneToLoad;                  // Scene to be loaded
    public Vector2 playerPosition;              // Player's position
    public VectorValue playerStoredPosition;    // Stored player's position

    [Header("Camera Settings")]
    public VectorValue cameraMin;               // Stored camera min value
    public VectorValue cameraMax;               // Stored camera max value
    public Vector2 cameraNewMin;                // New camera min value
    public Vector2 cameraNewMax;                // New camera max value

    [Header("Effects")]
    public GameObject fadeInPanel;              // Fade in transition panel
    public GameObject fadeOutPanel;             // Fade out transition panel
    public float fadeWaitTime;                  // Time to wait for fade animation
    

    private void Awake()
    {
        // If fade in has been assigned for scene
        if(fadeInPanel != null)
        {
            // Instantiate at position zero, with no rotation, as a GameObject
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;

            // Destroy the panel after 1 second
            Destroy(panel, 1);
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        // If collider is Player and is not trigger
        if(collider.CompareTag("Player") && !collider.isTrigger)
        {
            // Set player position to stored position
            playerStoredPosition.initValue = playerPosition;

            // Start the coroutine
            StartCoroutine(FadeCoroutine());
        }
    }

    public IEnumerator FadeCoroutine()
    {
        // If fade out has been assigned for scene
        if (fadeOutPanel != null)
        {
            // Instantiate the panel in game
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }

        // Wait for fadeWaitTime
        yield return new WaitForSeconds(fadeWaitTime);

        // Reset the camera bounds
        ResetCameraBounds();

        // Create a new async operation that loads specified scene
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);


        // Do nothing until asyncOp is done
        while(!asyncOperation.isDone)
        {
            // Wait for 1 frame
            yield return null;
        }
    }

    public void ResetCameraBounds()
    {
        // Resets camera bounds
        cameraMax.initValue = cameraNewMax;
        cameraMin.initValue = cameraNewMin;
    }

}