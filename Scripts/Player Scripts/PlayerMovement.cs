using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// State machine defines the states player can be in
/// </summary>
public enum PlayerState
{
    idle,
    walk,
    attack,
    stagger,
    interact
}

/// <summary>
/// Controls player movement, damage, and game over
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [Header("Player Attributes")]
    public float playerSpeed;                   // How fast the player moves, default is 0.075
    public float deathEffectDelay;              // Delay of death effect
    public FloatValue currentHealth;            // Current health of the player
    public Inventory playerInventory;           // Player's inventory
    public VectorValue startingPosition;        // Starting position of the player, when the game starts
    public GameObject deathEffect;

    [Header("Player State Machine")]
    public PlayerState currentState;            // Current state the player is in

    [Header("UI Settings")]
    public GameObject hudCanvas;                // HUD
    public GameObject deathCanvas;              // Game over screen
    public PersistenceManager manager;          // Persistence manager

    [Header("Invulnerability Frame Settings")]
    public SpriteRenderer playerSprite;         // Sprite renderer of the player
    public Collider2D triggerCollider;          // Trigger collider
    public Color flashColor;                    // Color of flash
    public Color defaultColor;                  // Default player color
    public float duration;                      // Flash duration
    public int flashesNumber;                   // Number of flashes

    [Header("Signals")]
    public Signal playerHealthSignal;           // Signal for updating the player health
    public Signal playerIsHit;                  // Signal for when player takes damage
    public Signal removeMana;                   // Remove mana signal

    [Header("Object Settings")]
    private Rigidbody2D myRigidbody;            // Rigidbody of the player
    private Vector3 changeRate;                 // Rate of change
    private Animator myAnimator;                // Animator of the player
    public SpriteRenderer receivedItemSprite;   // Sprite for when player receives an item

    [Header("Weapon Settings")]
    public GameObject projectile;               // Arrow
    public Item bowWeapon;                      // Bow!
    public Item swordWeapon;                    // Sword!

    void Start()
    {
        // Set current state of player
        currentState = PlayerState.walk;

        // Sets player rigidbody
        myRigidbody = GetComponent<Rigidbody2D>();

        // Sets player animator
        myAnimator = GetComponent<Animator>();

        // Set animator moveX value
        myAnimator.SetFloat("moveX", 0f);

        // Set animator moveY value
        myAnimator.SetFloat("moveY", -1f);

        // Start moving player
        transform.position = startingPosition.initValue;
    }

    void Update()
    {
        // Is the player in an interaction
        if(currentState == PlayerState.interact)
        {
            // Do nothing
            return;
        }

        // Set change rate
        changeRate = Vector3.zero;

        // Get input
        changeRate.x = Input.GetAxisRaw("Horizontal");
        changeRate.y = Input.GetAxisRaw("Vertical");

        // If player is pressing attack button and is not already attacking and is not staggered
        if(Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            // If player has a sword
            if(playerInventory.ItemCheck(swordWeapon))
            {
                // Start attack coroutine
                StartCoroutine(AttackCoroutine());
            }
        }
        else if(Input.GetButtonDown("Alternate Attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            // If player is pressing alternate attack button and is not already attacking and is not staggered

            // If player has a bow
            if (playerInventory.ItemCheck(bowWeapon))
            {
                // Start alt attack coroutine
                StartCoroutine(AlternateAttackCoroutine());
            }
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            // If current state is walk or idle

            // Move
            UpdateAnimationAndMove();
        }
    }

    public IEnumerator AttackCoroutine()
    {
        // Play attacking animation
        myAnimator.SetBool("attacking", true);

        // Set current state to attack
        currentState = PlayerState.attack;

        // Wait 1 frame
        yield return null;

        // Stop playing attacking animation
        myAnimator.SetBool("attacking", false);

        // Wait for 1/3 of a second
        yield return new WaitForSeconds(0.3f);

        // If player is not interacting
        if (currentState != PlayerState.interact)
        {
            // Set player state to walk
            currentState = PlayerState.walk;
        }
    }

    public IEnumerator AlternateAttackCoroutine()
    {
        // Set state to attack
        currentState = PlayerState.attack;

        // Wait 1 frame
        yield return null;

        // Create an arrow
        CreateProjectile();

        // Wait for 1/3 of a second
        yield return new WaitForSeconds(0.3f);

        // If player is not interacting
        if (currentState != PlayerState.interact)
        {
            // Set player state to walk
            currentState = PlayerState.walk;
        }
    }

    private void CreateProjectile()
    {
        // If player has mana
        if(playerInventory.currentMana > 0)
        {
            // Create vector facing in player direction
            Vector2 t = new Vector2(myAnimator.GetFloat("moveX"), myAnimator.GetFloat("moveY"));

            // Instantiate a new arrow as projectile, at player position, and player rotation,
            // and set the arrow component equal to arrow object
            Arrow arrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Arrow>();

            // Setup the arrow
            arrow.Setup(t, ProjectileDirection());

            // Remove the amount of mana that the arrow costs
            playerInventory.RemoveMana(arrow.arrowCost);

            // Raise signal
            removeMana.RaiseSignal();
        }   
    }

    Vector3 ProjectileDirection()
    {
        // Returns an angle in radians
        float t = Mathf.Atan2(myAnimator.GetFloat("moveY"), myAnimator.GetFloat("moveX")) * Mathf.Rad2Deg;

        // Returns the rotation the projectile should have based on current player direction
        return new Vector3(0, 0, t);
    }

    private IEnumerator KnockbackRoutine(float knockTime)
    {
        // If the rigidbody exists
        if (myRigidbody != null)
        {
            // Start the invulnerability frame coroutine
            StartCoroutine(FlashCoroutine());

            // Wait for knock time seconds
            yield return new WaitForSeconds(knockTime);

            // Set rigidbody velocity to zero
            myRigidbody.velocity = Vector2.zero;
            
            // Set player's state to idle
            currentState = PlayerState.idle;

            // Set rigidbody velocity to zero
            myRigidbody.velocity = Vector2.zero;
        }
    }

    public void RaiseItem()
    {
        // If player has a current item
        if (playerInventory.currentItem != null)
        {
            // If player is not interacting
            if (currentState != PlayerState.interact)
            {
                // Play raise item animation
                myAnimator.SetBool("receiveItem", true);

                // Set players current state to interact
                currentState = PlayerState.interact;

                // Set the sprite to item's sprite
                receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {
                // Don't play animation
                myAnimator.SetBool("receiveItem", false);

                // Set player's current state to idle
                currentState = PlayerState.idle;

                // Set sprite to null
                receivedItemSprite.sprite = null;

                // Set current item to null
                playerInventory.currentItem = null;
            }
        }
    }

    void UpdateAnimationAndMove()
    {
        // If change rate is not zero
        if (changeRate != Vector3.zero)
        {
            // Move
            MoveCharacter();

            // Set animator values
            myAnimator.SetFloat("moveX", changeRate.x);
            myAnimator.SetFloat("moveY", changeRate.y);

            // Play moving animation
            myAnimator.SetBool("moving", true);
        }
        else
        {
            // Stop playing animation
            myAnimator.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        // Normalize change rate
        changeRate.Normalize();

        // Fixed delta time works, delta time doesn't
        // Move the rigidbody
        myRigidbody.MovePosition(transform.position + changeRate * playerSpeed * Time.fixedDeltaTime);
    }

    public void Knock(float kT, float damage)
    {
        // Raise the signal to trigger the screen shake effect
        playerIsHit.RaiseSignal();

        // Applies damage value to player health
        currentHealth.RuntimeValue -= damage;

        // Raises signal to update player health
        playerHealthSignal.RaiseSignal();

        // If player still has health
        if (currentHealth.RuntimeValue > 0)
        {   
            // Start knockback coroutine
            StartCoroutine(KnockbackRoutine(kT));
        }
        else
        {
            // Start the death coroutine
            StartCoroutine(DeathCoroutine());

            // Deactivable HUD
            hudCanvas.SetActive(false);

            // Activate game over screen
            deathCanvas.SetActive(true);

            // Deactivate player
            this.gameObject.SetActive(false);
        }    
    }

    private IEnumerator DeathCoroutine()
    {
        // If death effect exists
        if (deathEffect != null)
        {
            // Create a deathEffect game object at the player's current position
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);

            // Destroy the death effect after deathEffectDelay seconds
            Destroy(effect, deathEffectDelay);
        }

        // Wait for 5 seconds
        yield return new WaitForSeconds(5);
    }

    private IEnumerator FlashCoroutine()
    {
        // Stops player from taking damage/knockback
        triggerCollider.enabled = false;

        // Temp counter
        int t = 0;

        // While there are still flashes
        while (t < flashesNumber)
        {
            // Change player color to flash color
            playerSprite.color = flashColor;

            // Wait for duration seconds
            yield return new WaitForSeconds(duration);

            // Change player color back to default color
            playerSprite.color = defaultColor;

            // Wait for duration seconds
            yield return new WaitForSeconds(duration);

            // Increment counter
            t++;
        }

        // Re-enable collider so player can take damage
        triggerCollider.enabled = true;
    }

    public void QuitToMenu()
    {
        if(manager != null)
        {
            // Delete save files
            manager.ResetScriptableObjects();
        }
        else
        {
            int i = 0;
            // If the save file exists
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", i)))
            {
                while (File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", i)))
                {
                    // Delete the file
                    File.Delete(Application.persistentDataPath + string.Format("/{0}.dat", i));
                    i++;
                }
            }
        }

        // Load main menu scene
        SceneManager.LoadScene("MainMenu");
    }
}