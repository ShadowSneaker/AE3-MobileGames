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

    // if the item cant be consumed it displays the information
    public bool DisplayInfo = false;

    //description of the item
    public string Description;


	public virtual void Use(Entity User)
    {
        if(ConsumeOnUse)
        {
            if(ItemName == "Potion")
            {
                User.ApplyHeal(2);
            }
        }
        else
        {
            DisplayInfo = true;
        }
    }

    
}
