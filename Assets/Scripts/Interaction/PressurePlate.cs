using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public ActivatableObject[] BoundObjects;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity Other = collision.gameObject.GetComponent<Entity>();
        if (Other)
        {
            for (int i = 0; i < BoundObjects.Length; ++i)
            {
                BoundObjects[i].Activate();
            }
        }
    }
}
