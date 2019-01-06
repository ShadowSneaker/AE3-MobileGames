using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : Abilitiy
{
    public float AbilityDuration;

    private bool Active;
	
	private IEnumerator Duration()
    {
        Owner.Reflect = true;
        yield return new WaitForSeconds(AbilityDuration);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Projectile Other = collision.gameObject.GetComponent<Projectile>();
        if (Other)
        {
            Rigidbody2D OtherRigid = Other.GetComponent<Rigidbody2D>();
            OtherRigid.velocity = -OtherRigid.velocity;
        }
    }
}
