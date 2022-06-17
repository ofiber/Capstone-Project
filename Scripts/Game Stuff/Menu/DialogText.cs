using UnityEngine;
using TMPro;

/// <summary>
/// DialogText prefab controller
/// </summary>
public class DialogText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogText;    // Text of the dialog

    public void SetupText(string dialog)
    {
        // Set the dialogText to dialog
        dialogText.text = dialog;
    }
}