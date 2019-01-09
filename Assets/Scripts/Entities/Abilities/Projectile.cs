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

    public bool ReturnToCaster;

    public float ReturnSpeed = 200.0f;

    public bool DestroyOnEnemy = true;

    private Rigidbody2D Rigid;

    private int BounceIndex = 0;

    private bool Return;
    private bool ReturnActive;

    //internal bool OverrideSpeed; 
    internal GameObject Owner;

    internal bool Reverse;


    protected virtual void Start()
    {
        Rigid = GetComponent<Rigidbody2D>();
        Rigid.AddForce(transform.right * Speed * ((Reverse) ? -1.0f : 1.0f));
        StartCoroutine(EnableReturn());
    }

    private IEnumerator EnableReturn()
    {
        yield return new WaitForSeconds(0.1f);
        ReturnActive = true;
    }


    private void Update()
    {
        if (Return)
        {
            transform.position = Vector2.MoveTowards(transform.position, Owner.transform.position, ReturnSpeed * Time.deltaTime);
        }
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
        Entity Other = collision.GetComponent<Entity>();
        if (Owner && Other && Owner != Other.gameObject)
        {
            if (OnlyHitPlayer && !Other.CompareTag("Player"))
            {
                return;
            }
            Other.ApplyDamage(Damage, null);

            if (DestroyOnEnemy)
            {
                Destroy(gameObject);
            }
        }
        else if (CanBounce && collision.CompareTag("Floor"))
        {
            if (++BounceIndex > BounceCount)
            {
                Destroy(gameObject);
            }
        }
        else if (ReturnToCaster && ReturnActive)
        {
            if (collision.gameObject == Owner && Return)
            {
                Destroy(gameObject);
            }
            

            Return = true;
            Rigid.velocity = Vector2.zero;
        }
    }
}
