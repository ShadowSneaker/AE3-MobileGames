using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : InteractableObject
{
    public bool Interactable;

    public Teleporter Destination;

    private float YOffset;

    private Entity EntityToTeleport;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Active)
        {
            Entity Other = collision.gameObject.GetComponent<Entity>();
            if (Other)
            {
                EntityToTeleport = Other;

                if (!Interactable)
                {
                    Teleport();
                }
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        Entity Other = collision.gameObject.GetComponent<Entity>();
        if (Other)
        {
            Active = true;
        }
    }


    public override void Interact()
    {
        if (Interactable)
        {
            base.Interact();
            Teleport();
        }
    }


    public void Teleport()
    {
        Destination.Active = false;
        YOffset = EntityToTeleport.transform.position.y - transform.position.y;
        EntityToTeleport.transform.position = Destination.transform.position + new Vector3(0.0f, YOffset, 0.0f);
    }
}
