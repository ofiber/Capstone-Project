using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// DEPRECIATED -> Cinemachine handles this now
/// Controls transfer of player and camera between game 'rooms'
/// </summary>
public class RoomTransfer : MonoBehaviour
{
    [Header("Camera Settings")]
    public Vector2 cameraChange;        // Amount to change camera
    public Vector3 playerChange;        // Amount to change player

    [Header("Room Name Settings")]
    public bool needText;               // Does this room need text?
    public string placeName;            // What is the text
    public GameObject text;             // Text object
    public Text placeText;              // Text object's text component

    private CameraMovement cam;         // Camera to move

    void Start()
    {
        // Set camera
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // If collider tag is player and not trigger
        if (collider.CompareTag("Player") && !collider.isTrigger)
        {
            // Change camera min position
            cam.minPosition += cameraChange;

            // Change camera max position
            cam.maxPosition += cameraChange;

            // Move player
            collider.transform.position += playerChange;

            // If game 'room' needs text
            if (needText)
            {
                // Start text coroutine
                StartCoroutine(PlaceNameRoutine());
            }
        }
    }

    private IEnumerator PlaceNameRoutine()
    {
        // Activate text object
        text.SetActive(true);

        // Set text
        placeText.text = placeName;

        // Wait for 4 seconds
        yield return new WaitForSeconds(4);

        // Deactivate text object
        text.SetActive(false);
    }
}