using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool OnlyHitPlayer = true;

    public float Speed = 200.0f;

    public int Damage = 1;


    public bool CanBounce;

    // How many bounces the projectile will do before being deleted.
    public int BounceCount;

    private Rigidbody2D Rigid;


    public int BounceIndex = 0;

    //internal bool OverrideSpeed; 
    internal GameObject Owner;

    internal bool Reverse;


    protected virtual void Start()
    {
        Rigid = GetComponent<Rigidbody2D>();
        Rigid.AddForce(transform.right * Speed * ((Reverse) ? -1.0f : 1.0f));
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollisionHit(collision);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionHit(collision.collider);
    }


    private void CollisionHit(Collider2D collision)
    {
        Entity Other = collision.gameObject.GetComponent<Entity>();
        if (Owner && Other && Owner != Other.gameObject)
        {
            if (OnlyHitPlayer && !Other.CompareTag("Player"))
            {
                return;
            }
            Other.ApplyDamage(Damage, null);
            Destroy(gameObject);
        }
        else if (CanBounce && collision.CompareTag("Floor"))
        {
            if (++BounceIndex > BounceCount)
            {
                Destroy(gameObject);
            }
        }
    }
}
