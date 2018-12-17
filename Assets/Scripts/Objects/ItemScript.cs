using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemScript : ScriptableObject
{
    public bool ConsumeOnUse = true;
    public Mesh Object;


	public virtual void Use()
    {
        if (ConsumeOnUse)
        {

        }
    }
}
