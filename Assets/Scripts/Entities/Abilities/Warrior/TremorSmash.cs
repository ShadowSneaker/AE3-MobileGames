using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TremorSmash : Abilitiy
{
    public int MinDamage;
    public int MaxDamage;

    private bool Activated;
    private CameraShake Shake;

    protected override void Start()
    {
        base.Start();
        Shake = GetComponent<CameraShake>();
    }

    public override void CastAbility()
    {
        if (GetAbilityUp)
        {
            base.CastAbility();

            Activated = true;

            if (Owner.OnGround())
            {
                Owner.Jump();
            }
        }
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (Activated)
        {
            Entity Other = collision.gameObject.GetComponent<Entity>();
            if (Other)
            {
                Other.ApplyDamage(Random.Range(MinDamage, MaxDamage), Owner);
                Shake.Play = true;
                Activated = false;
                EndAbility();
            }
            else if (collision.gameObject.CompareTag("Floor"))
            {
                Shake.Play = true;
                Activated = false;
                EndAbility();
            }
        }
    }
}
