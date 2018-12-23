using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Teleporter Destination;

    private bool CanTeleport = true;

    private float YOffset;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CanTeleport)
        {
            Entity Other = collision.gameObject.GetComponent<Entity>();
            if (Other)
            {
                Destination.CanTeleport = false;
                YOffset = Other.transform.position.y - transform.position.y;
                Other.transform.position = Destination.transform.position + new Vector3(0.0f, YOffset, 0.0f);
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        Entity Other = collision.gameObject.GetComponent<Entity>();
        if (Other)
        {
            CanTeleport = true;
        }
    }
}
