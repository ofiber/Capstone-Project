using UnityEngine;

/// <summary>
/// Text asset value used for Ink branching dialog
/// </summary>
[CreateAssetMenu(fileName = "NewTextAsset", menuName = "Scriptable Values/Text Asset Value")]
public class TextAssetValue : ScriptableObject
{
    public TextAsset value; // The text value
}