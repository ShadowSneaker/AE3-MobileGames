using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemScript : ScriptableObject
{
    public bool ConsumeOnUse = true;
    public Sprite Image;

    public string ItemName;

	public virtual void Use(Entity User)
    {
        
    }
}
