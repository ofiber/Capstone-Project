using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

/// <summary>
/// Controls the branching dialog system
/// </summary>
public class BranchingDialogController : MonoBehaviour
{
    [Header("Dialog Control Settings")]
    [SerializeField] private GameObject dialogCanvas;           // The canvas that displays the dialog
    [SerializeField] private GameObject dialogTextPrefab;       // The prefab that shows the text from the dialog
    [SerializeField] private GameObject dialogChoicePrefab;     // The prefab that shows the dialog choices
    [SerializeField] private GameObject dialogTextContent;      // The scroll area that holds the text prefabs
    [SerializeField] private GameObject dialogChoiceContent;    // The scroll area that holds the choice prefabs
    [SerializeField] private TextAssetValue interValue;         // The interim dialog value
    [SerializeField] private ScrollRect dialogTextScroll;       // The text scroll

    // Using Ink for creating the dialogs
    [Header("Ink Story Settings")]
    [SerializeField] private Story story;                       // The dialog 'Story'

    public void EnableCanvas()
    {
        // Set the dialog canvas as active
        dialogCanvas.SetActive(true);

        // Setup the story
        SetupStory();

        // Refresh the dialog area
        RefreshView();
    }
    
    public void DisableCanvas()
    {
        dialogCanvas.SetActive(false);
    }

    // Set up the Ink story dialog
    public void SetupStory()
    {
        // If the interValue exists
        if(interValue.value != null)
        {
            // Will delete any old dialog from dialog window
            DeleteOldDialog();

            // If story exists, create story
            story = new Story(interValue.value.text);
        }
        else
        {
            // Else do nothing
            // Except show a debug but it only shows in the editor
            Debug.Log("Story setup failed: Story value was NULL");
        }
    }

    public void DeleteOldDialog()
    {
        // Delete all of the dialog in the dialog scroll area
        for(int i = 0; i < dialogTextContent.transform.childCount; i++)
        {
            Destroy(dialogTextContent.transform.GetChild(i).gameObject);
        }
    }

    public void RefreshView()
    {
        // If there is more to the story
        while(story.canContinue)
        {
            // Create new dialog and move the story along
            MakeDialog(story.Continue());
        }

        // If there are choices in the story
        if(story.currentChoices.Count > 0)
        {
            // Create choices
            MakeChoices();
        }
        else
        {
            // Otherwise, disable the canvas
            dialogCanvas.SetActive(false);
        }

        // Start the scroll coroutine
        StartCoroutine(ScrollCoroutine());
    }

    IEnumerator ScrollCoroutine()
    {
        // Waits 1 frame
        yield return null;

        // Move the scroll down so it's always showing the most recent text
        dialogTextScroll.verticalNormalizedPosition = 0f;
    }
    
    public void MakeDialog(string newDialogString)
    {
        // Instantiate a new dialog text prefab
        DialogText newDialog = Instantiate(dialogTextPrefab, dialogTextContent.transform).GetComponent<DialogText>();

        // Setup the new dialog
        newDialog.SetupText(newDialogString);
    }

    public void MakeChoices()
    {
        // Destroy old choices
        for (int i = 0; i < dialogChoiceContent.transform.childCount; i++)
        {
            Destroy(dialogChoiceContent.transform.GetChild(i).gameObject);
        }

        // Create new choices
        for (int i = 0; i < story.currentChoices.Count; i++)
        {
            SetupChoices(story.currentChoices[i].text, i);
        }
    }

    public void SetupChoices(string newChoiceText, int index)
    {
        // Instantiate a new dialog choice prefab
        DialogChoice newChoice = Instantiate(dialogChoicePrefab, dialogChoiceContent.transform).GetComponent<DialogChoice>();

        // Setup the button
        newChoice.SetupButton(newChoiceText, index);

        // Get the button component
        Button butt = newChoice.gameObject.GetComponent<Button>();

        // If the button exists
        if(butt != null)
        {
            // Adds the ChoiceChoice method as the method to be called when the button is clicked
            butt.onClick.AddListener(delegate
            { 
                ChooseChoice(index);
            });
        }
    }

    public void ChooseChoice(int choice)
    {
        // When player clicks a choice
        story.ChooseChoiceIndex(choice);

        // Refresh the view
        RefreshView();
    }
}