using UnityEngine;

/// <summary>
/// State machine containing different door types
/// </summary>
public enum DoorType
{
    overworldKey,    // Door that requires a normal key item
    dungeonKey,     // Requires special dungeon key item
    enemy,         // Door that opens once all the enemies in the area are killed
    button        // Door that requires a button press to open?
}

/// <summary>
/// Defines a door that can be opened by the player
/// </summary>
public class Door : Sign
{
    [Header("Door Attributes")]              // Usefull for organization of variables in inspector
    public DoorType thisDoorType;           // This door's type
    public bool isOpen = false;             // Is the door open?

    [Header("Object Attributes")]
    public SpriteRenderer doorSprite;       // Sprite of the door
    public BoxCollider2D physicsCollider;   // Collider of the door

    [Header("Player's Inventory")]
    public Inventory playerInventory;       // Player's inventory
    public Item dungeonKey;                 // Special dungeon key

    public override void Update()
    {
        // If player presses 'F' key
        if(Input.GetKeyDown(KeyCode.F))
        {
            // If player is in range and the door type is overworld key
            if(playerInRange && thisDoorType == DoorType.overworldKey)
            {
                // Does the player have an overworld key?
                if(playerInventory.numberOfKeys > 0)
                {
                    // Remove the key
                    //playerInventory.numberOfKeys--;

                    // Open
                    Open();
                }
                else
                {
                    if (dialogBox.activeInHierarchy)
                    {
                        // Deactivate it
                        dialogBox.SetActive(false);
                    }
                    else
                    {
                        // Otherwise, active dialog box
                        dialogBox.SetActive(true);

                        // Set the dialog text
                        dialogText.text = dialog;
                    }
                }
            }

            // If player is in range and the door type is dungeon key
            if(playerInRange && thisDoorType == DoorType.dungeonKey)
            {
                // Does the player have a dungeon key?
                if(playerInventory.ItemCheck(dungeonKey))
                {
                    // Open
                    Open();
                }
                else
                {
                    if (dialogBox.activeInHierarchy)
                    {
                        // Deactivate it
                        dialogBox.SetActive(false);
                    }
                    else
                    {
                        // Otherwise, active dialog box
                        dialogBox.SetActive(true);

                        // Set the dialog text
                        dialogText.text = dialog;
                    }
                }
            }
        }
    }

    public void Open()
    {
        // Turn off the door's sprite renderer
        doorSprite.enabled = false;

        // Set isOpen to true
        isOpen = true;

        // Turn off the door's box collider
        physicsCollider.enabled = false;
    }

    public void Close()
    {
        // Turn on the door's sprite renderer
        doorSprite.enabled = true;

        // Set isOpen to false
        isOpen = false;

        // Turn on the door's box collider
        physicsCollider.enabled = true;
    }
}