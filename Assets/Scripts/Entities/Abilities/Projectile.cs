using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 200.0f;

    public int Damage = 1;

    private Rigidbody2D Rigid;




    protected virtual void Start()
    {
        Rigid = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        //Rigid.velocity = transform.forward * Speed * Time.deltaTime;
        transform.position += transform.right * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity Other = collision.gameObject.GetComponent<Entity>();
        if (Other)
        {
            Other.ApplyDamage(Damage);
            Destroy(gameObject);
        }
    }

}
