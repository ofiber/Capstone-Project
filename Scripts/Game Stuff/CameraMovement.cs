using System.Collections;
using UnityEngine;

/// <summary>
/// DEPRECIATED -> Switched to Cinemachine because it was much smoother
/// This script controlled the movement of the camera to follow the player
/// </summary>
public class CameraMovement : MonoBehaviour
{
    [Header("Animator")]
    public Animator animator;           // Animator

    [Header("Camera Settings")]
    public Transform target;            // Target -> player
    public float smoothing;             // Smoothing rate

    [Header("Camera Position")]
    public Vector2 maxPosition;         // Max position of camera
    public Vector2 minPosition;         // Min position of camera

    [Header("Position Reset Settings")]
    public VectorValue cameraMin;       // Position to reset camera to
    public VectorValue cameraMax;       // Position to reset camera to

    private void Start()
    {
        // Set max position
        maxPosition = cameraMax.initValue;

        // Set minPosition
        minPosition = cameraMin.initValue;

        // Sets animator to camera's animator
        animator = GetComponent<Animator>();
    }

    // LateUpdate makes camera nice and smooth :)
    void LateUpdate()
    {

        if (transform.position != target.position)
        {
            // Move camera to target
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }

    public void BeginKick()
    {
        // Play screen kick animation -> effect for when player gets hit
        animator.SetBool("kickActive", true);

        // Start coroutine
        StartCoroutine(ScreenKickCoroutine());
    }

    public IEnumerator ScreenKickCoroutine()
    {
        // Wait 1 frame
        yield return null;

        // Stop screen kick animation
        animator.SetBool("kickActive", false);
    }
}