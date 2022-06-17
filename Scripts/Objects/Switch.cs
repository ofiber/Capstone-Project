using UnityEngine;

/// <summary>
/// Controls a switch that can open a door
/// </summary>
public class Switch : MonoBehaviour
{
    [Header("Bool Values")]
    public bool isActive;                       // Is the switch active
    public BooleanValue persistentValue;        // Allows state of switch to persist between loading scenes

    [Header("Connected Door")]
    public Door connectedDoor;                  // The door that this switch is connected to

    [Header("Sprite Settings")]
    public Sprite activeSprite;                 // Switch active sprite
    private SpriteRenderer spriteRenderer;      // Switch's sprite renderer

    private void Start()
    {
        // Set sprite renderer equal to this object's sprte renderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set isActive to persistent value of switch
        isActive = persistentValue.initVal;

        // Activates switch when reloading scene if switch has been previously activated
        if(isActive)
        {
            ActivateSwitch();
        }

    }

    public void ActivateSwitch()
    {
        // Activate switch
        isActive = true;

        // Set the persisten value
        persistentValue.RuntimeValue = isActive;

        // Open the door that's connected to this switch
        connectedDoor.Open();

        // Change sprite to switch active sprite
        spriteRenderer.sprite = activeSprite;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        // If the collider is player
        if(collider.CompareTag("Player"))
        {
            // If player enters collision trigger area, activate switch
            ActivateSwitch();
        }
    }
}