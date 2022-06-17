using UnityEngine;

/// <summary>
/// Scriptable boolean object
/// </summary>
[CreateAssetMenu(fileName = "NewBooleanValue", menuName = "Scriptable Values/Boolean Value")]
[System.Serializable]
public class BooleanValue : ScriptableObject
{
    [Header("Boolean Value Settings")]
    public bool initVal;        // Initial value
    public bool RuntimeValue;   // Value during runtime
}