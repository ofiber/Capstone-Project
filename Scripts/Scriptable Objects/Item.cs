using UnityEngine;

/// <summary>
/// Defines an item that can be stored in player inventory
/// </summary>
[CreateAssetMenu(fileName = "NewScriptableItem", menuName = "Item")]
[System.Serializable]
public class Item : ScriptableObject
{
    public Sprite itemSprite;   // Sprite of the item
    public string itemDesc;     // Description of the item
    public bool isKey;          // Is this item a key?
    public bool isDungeonKey;   // Is this item a dungeon key?
}