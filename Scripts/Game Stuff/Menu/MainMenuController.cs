using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the main menu
/// </summary>
public class MainMenuController : MonoBehaviour
{
    [Header("Button Settings")]
    public int index;                       // Index of the button
    public bool keyDown;                    // Is the key down?
    public int maxIndex;                    // Max index. ie: total number of buttons
    public GameObject continueButton;       // Continue game button

    private void OnEnable()
    {
        // If data files exist, show the continue game button
        if(File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", 0)))
        {
            continueButton.SetActive(true);
        }
    }

    private void Update()
    {
        // If the player is using a vertical axis key
        if (Input.GetAxis("Vertical") != 0)
        {
            // If key is not down
            if (!keyDown)
            {
                // If players are moving up
                if (Input.GetAxis("Vertical") < 0)
                {
                    if (index < maxIndex)
                    {
                        // Increase index
                        index++;
                    }
                    else
                    {
                        // Set index to zero
                        index = 0;
                    }
                }
                else if (Input.GetAxis("Vertical") > 0)
                {
                    if (index > 0)
                    {
                        // Decrease index
                        index--;
                    }
                    else
                    {
                        // Index equals maxIndex
                        index = maxIndex;
                    }
                }

                // Set key down to true
                keyDown = true;
            }
        }
        else
        {
            // Set key down to false
            keyDown = false;
        }
    }

    public void NewGameButton()
    {
        // If player clicks the new game button, load the opening cutscene
        SceneManager.LoadScene("OpenCutscene");
    }

    public void ContinueGameButton()
    {
        // If the player clicks the continue game button, load the overworld scene
        SceneManager.LoadScene("Overworld");
    }

    public void QuitGameButton()
    {
        // If the player clicks the quit game button, quit the application
        Application.Quit();
    }
}