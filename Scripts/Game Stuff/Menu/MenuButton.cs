using UnityEngine;

/// <summary>
/// Defines a button used in menus
/// </summary>
public class MenuButton : MonoBehaviour
{
    public MainMenuController menuControl;  // Controller of the main menu
    public Animator anim;                   // Animator of the button
    public int thisIndex;                   // Index of this button

    private void Update()
    {
        if(menuControl.index == thisIndex)
        {
            // Play selected animation
            anim.SetBool("selected", true);
            
            if(Input.GetAxis("Submit") == 1)
            {
                // Play the pressed animation
                anim.SetBool("pressed", true);

                if(thisIndex == 0)
                {
                    // Call the new game method
                    menuControl.NewGameButton();
                }
                else if(thisIndex == 1)
                {
                    // Call the continue game method
                    menuControl.ContinueGameButton();
                }
                else if(thisIndex == 2)
                {
                    // Cal the quit game method
                    menuControl.QuitGameButton();
                }
            }
            else if(anim.GetBool("pressed"))
            {
                // Don't play the pressed animation
                anim.SetBool("pressed", false);
            }
        }
        else
        {
            // Don't play the selected animation
            anim.SetBool("selected", false);
        }
    }
}