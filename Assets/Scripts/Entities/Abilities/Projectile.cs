using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float Speed = 200.0f;

    public int Damage = 1;

    private Rigidbody2D Rigid;

    //internal bool OverrideSpeed; 
    internal GameObject Owner;


    protected virtual void Start()
    {
        Rigid = GetComponent<Rigidbody2D>();
        Rigid.AddForce(transform.right * Speed);
    }

    private void Update()
    {
        //if (OverrideSpeed)
        //{
        //    OverrideSpeed = false;
        //    Rigid.AddForce(transform.right * Speed);
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity Other = collision.gameObject.GetComponent<Entity>();
        if (Owner && Other && Owner != Other.gameObject)
        {
            Other.ApplyDamage(Damage);
            Destroy(gameObject);
        }
    }

}
