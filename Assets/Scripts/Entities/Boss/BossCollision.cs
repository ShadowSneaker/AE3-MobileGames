using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollision : MonoBehaviour {

    public int DashDamage;

    private bool Dashing;
    private Entity This;

    private void Start()
    {
        This = GetComponent<Entity>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Dashing)
        {
            Entity Other = collision.GetComponent<Entity>();
            if (Other)
            {
                Other.ApplyDamage(DashDamage, This);
            }
        }

        if (collision.CompareTag("BossWall"))
        Dashing = false;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("BossWall"))
        {
            Dashing = true;
        }
    }
}
