using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursedWall : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity Other = collision.GetComponent<Entity>();
        if (Other)
        {
            Other.ApplyDamage(9999);
        }
    }
}
