using UnityEngine;
using TMPro;

/// <summary>
/// Defines a chest that contains an item and can be opened by player
/// </summary>
public class TreasureChest : Interactable
{
    [Header("Chest Attributes")]
    public Item contents;                   // What's inside the chest
    public bool isOpen;                     // Has the chest already been open?
    public BooleanValue persistentOpen;     // Allows the chests current state (open/closed) to persist between loading scenes

    [Header("Signals")]
    public Signal raiseItem;                // Signal to have player raise item

    [Header("Dialog Settings")]
    public GameObject dialogBox;            // Dialog box
    public TMP_Text dialogText;             // Text for dialog box

    [Header("Player's Inventory")]
    public Inventory playerInventory;       // Player's inventory

    private Animator animator;              // Chest animator

    private void Start()
    {
        // Set the animator
        animator = GetComponent<Animator>();

        // Set is open to the persistent value
        isOpen = persistentOpen.RuntimeValue;

        // If the chest is open
        if(isOpen)
        {
            // Play the open animation
            animator.SetBool("opened", true);
        }
    }

    private void Update()
    {
        // If the player presses 'F' and is in range
        if (Input.GetKeyDown(KeyCode.F) && playerInRange)
        {
            // If chest is not open
            if(!isOpen)
            {
                // Open the chest
                OpenChest();
            }
            else
            {
                // Otherwise, chest is already open :)
                ChestIsOpen();
            }
        }
    }

    public void OpenChest()
    {
        // Activate dialog box
        dialogBox.SetActive(true);

        // Set dialog text equal to contents text
        dialogText.text = contents.itemDesc;

        // Add contents of chest to player inventory
        playerInventory.AddItem(contents);

        // Set current item to chest contents -> ie: the item the player holds up
        playerInventory.currentItem = contents;

        // Raise the signal to animate player holding item
        raiseItem.RaiseSignal();

        // Raise the context clue so that it turns off
        context.RaiseSignal();

        // Set chest to opened
        isOpen = true;

        // Play open animation
        animator.SetBool("opened", true);

        // Sets persistent value for open chest
        persistentOpen.RuntimeValue = isOpen;
    }

    public void ChestIsOpen()
    {
        // Turn the dialog off
        dialogBox?.SetActive(false);

        // Set the current item to null
        //playerInventory.currentItem = null;

        // Raise the signal to stop animating player holding item
        raiseItem.RaiseSignal();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // If collision is Player and not trigger and chest is not open
        if (collider.CompareTag("Player") && !collider.isTrigger && !isOpen)
        {
            // Raise signal
            context.RaiseSignal();

            // Set player in range true
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        // If collision is Player and not trigger and chest is not open
        if (collider.CompareTag("Player") && !collider.isTrigger && !isOpen)
        {
            // Raise signal
            context.RaiseSignal();

            // Set player in range false
            playerInRange = false;
        }
    }
}