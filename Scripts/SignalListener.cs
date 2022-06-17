using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Defines a listener that listens for a signal
/// </summary>
public class SignalListener : MonoBehaviour
{
    [Header("Signal Listener Settings")]
    public Signal signal;           // Signal to listen for
    public UnityEvent sigEvent;     // Event to raise

    public void OnSignalRaised()
    {
        // When the signal listener is listening for is raised
        // Invoke the event
        sigEvent.Invoke();
    }

    private void OnEnable()
    {
        // Register the listener
        signal.RegisterListener(this);
    }

    private void OnDisable()
    {
        // Deregister the listener
        signal.DeregisterListener(this);
    }
}