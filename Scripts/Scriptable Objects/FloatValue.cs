using UnityEngine;

/// <summary>
/// Scriptable float value object
/// </summary>
[CreateAssetMenu(fileName = "NewFloatValue", menuName = "Scriptable Values/Float Value")]
[System.Serializable]
public class FloatValue : ScriptableObject
{
    [Header("Float Value Settings")]
    public float initVal;        // Initial value
    public float RuntimeValue;   // Value during runtime
}