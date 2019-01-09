using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : Abilitiy
{
    //public float AbilityDuration;
    public Color DurationColour;

    private bool Active;

    private SpriteRenderer Img;


    protected override void Start()
    {
        base.Start();
        Img = GetComponent<SpriteRenderer>();
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


    public override void CastAbility()
    {
        if (GetAbilityUp)
        {
            base.CastAbility();
            Img.color = DurationColour;
            Owner.Reflect = true;
            Owner.Attacking = false;
        }
    }


    public override void EndAbility()
    {

        base.EndAbility();
        Img.color = Color.white;
    }
}
