using UnityEngine;
using TMPro;

/// <summary>
/// DialogChoice button prefab controller
/// </summary>
public class DialogChoice : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonText;    // Text of the button
    [SerializeField] private int choiceIndex;               // Index of the button

    public void SetupButton(string newButtonText, int index)
    {
        // Set the button text to newButtonText
        buttonText.text = newButtonText;

        // Set choiceIndex to index
        choiceIndex = index;
    }
}