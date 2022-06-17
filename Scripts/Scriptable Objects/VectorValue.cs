using UnityEngine;

/// <summary>
/// Defines a scriptable vector object
/// </summary>
[CreateAssetMenu(fileName = "NewVectorValue", menuName = "Scriptable Values/Vector Value")]
public class VectorValue : ScriptableObject, ISerializationCallbackReceiver
{
    [Header("Vectors")]
    public Vector2 initValue;           // Initial value
    public Vector2 defaultValue;        // Default value

    public void OnAfterDeserialize()
    {
        // Set init to default
        initValue = defaultValue;
    }

    public void OnBeforeSerialize()
    {
        // This method is required by ISerializationCallbackReceiver, even if it doesn't do anything
    }
}