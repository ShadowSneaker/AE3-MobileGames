using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemScript : ScriptableObject
{
    // Should this item be removed when it is used.
    public bool ConsumeOnUse = true;

    // The sprite this item uses.
    public Sprite Image;

    // The display name of this item.
    public string ItemName;

    // How muchthis item is worth when selling.
    public int SellValue;



	public virtual void Use(Entity User)
    {
        
    }

    public virtual void Use()
    {
        // this will be where the item is then used.
        // for this it will display a description unless its a potion then used on the player
    }
}
