using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thwump : MonoBehaviour
{
    public int Damage = 9999;

    private Rigidbody2D Rigid;

    private bool Fallen;

	// Use this for initialization
	void Start ()
    {
        Rigid = GetComponent<Rigidbody2D>();
	}


    private void Update()
    {
        if (!Fallen)
        {
            RaycastHit2D Hit = Physics2D.Raycast(transform.position, Vector2.down);
            if (Hit.collider.CompareTag("Player"))
            {
                Rigid.simulated = true;
                Fallen = true;
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Entity Other = collision.gameObject.GetComponent<Entity>();
        if (Other)
        {
            Other.ApplyDamage(Damage, null);
        }

        GetComponent<CameraShake>().Play = true;
    }
}
