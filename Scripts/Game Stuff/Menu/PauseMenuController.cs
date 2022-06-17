using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the pause menu
/// </summary>
public class PauseMenuController : MonoBehaviour
{
    [Header("Pause Menu Settings")]
    public GameObject pausePanel;       // Pause panel
    public GameObject inventoryPanel;   // Inventory panel
    public GameObject hudCanvas;        // HUD
    public string sceneToLoad;          // Scene to load on quit game
    public bool isPaused;               // Is the game paused
    public bool inPause, inInv;         // In the pause menu, in the inventory menu


    private void Start()
    {
        // Set isPause and inPause to false
        isPaused = false;
        inPause = false;

        // Set pause and inventory panels inactive
        pausePanel.SetActive(false);
        inventoryPanel.SetActive(false);
    }

    private void Update()
    {
        // If player presses the pause button, default 'ESC'
        if(Input.GetButtonDown("pause"))
        {
            // Update pause
            UpdatePause();
        }
    }

    public void UpdatePause()
    {
        // Set is paused to the opposite of is paused
        isPaused = !isPaused;

        // If is pause is true
        if (isPaused)
        {
            // Activate the pause panel
            pausePanel.SetActive(true);

            // Deactivate the HUD
            hudCanvas.SetActive(false);

            // Set time scale to reeeaaaalllllyyyy slow
            Time.timeScale = 0.0001f;

            // Set inPause to true
            inPause = true;
        }
        else
        {
            // Deactivate pause panel
            pausePanel.SetActive(false);

            // Deactivate inventory panel
            inventoryPanel.SetActive(false);

            // Activate HUD
            hudCanvas.SetActive(true);

            // Reset time scale to normal time
            Time.timeScale = 1.0f;
        }
    }

    public void ResumeGame()
    {
        // Update
        UpdatePause();
    }

    public void QuitGame()
    {
        // Make sure time scale is default
        Time.timeScale = 1.0f;

        // Load the specified scene
        SceneManager.LoadScene(sceneToLoad);
    }

    public void SwitchDisplay()
    {
        // Set in pause to opposite of in pause
        inPause = !inPause;
        
        if(inPause)
        {
            // Activate pause panel
            pausePanel.SetActive(true);

            // Deactivate inventory panel
            inventoryPanel.SetActive(false);

            // Get the inventory panel animator
            Animator anim = inventoryPanel.GetComponent<Animator>();

            // Set the animator bool
            anim.SetBool("close", true);
        }
        else
        {
            // Activate inventory panel
            inventoryPanel.SetActive(true);

            // Deactivate pause panel
            pausePanel.SetActive(false);
        }
    }
}