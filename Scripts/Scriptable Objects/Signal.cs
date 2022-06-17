using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines how signals work in this game
/// </summary>
[CreateAssetMenu(menuName = "MySignal")]
public class Signal : ScriptableObject
{
    public List<SignalListener> listeners = new List<SignalListener>(); // The signal listeners that are listening for this signal

    public void RaiseSignal()
    {
        // For all the listeners in the listr
        for(int i = listeners.Count - 1; i >= 0; i--)
        {
            // Do the action specified by the listener
            listeners[i].OnSignalRaised();
        }
    }

    public void RegisterListener(SignalListener listener)
    {
        // Add a listener
        listeners.Add(listener);
    }

    public void DeregisterListener(SignalListener listener)
    {
        // Remove a listener
        listeners.Remove(listener);
    }
}