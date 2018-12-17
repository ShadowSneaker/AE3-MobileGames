using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : InteractableObject
{
    public ItemScript Item;

    public override void Interact()
    {
        base.Interact();

        // Put Item into Inventory.
    }
}
