using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : Entity
{
    public bool AlwaysAttack;

    public bool SearchBehind = true;
    public float RaycastLength = Mathf.Infinity;


	// Use this for initialization
	protected override void Start ()
    {
        base.Start();


	}


    private void Update()
    {
        if (Abilities[0].GetAbilityUp)
        {
            if (AlwaysAttack)
            {
                UseAbility(0);
            }
            else
            {
                RaycastHit2D Hit = Physics2D.Raycast(transform.position + (new Vector3(Offset + 0.2f, 0.0f, 0.0f) * transform.localScale.x), Vector2.right * transform.localScale.x);
                if (Hit)
                {
                    if (Hit.collider.CompareTag("Player"))
                    {
                        UseAbility(0);
                    }
                    else if (SearchBehind)
                    {
                        RaycastHit2D BehindHit = Physics2D.Raycast(transform.position + (new Vector3(Offset + 0.4f, 0.0f, 0.0f) * -transform.localScale.x), Vector2.left * transform.localScale);
                        if (BehindHit)
                        {
                            if (BehindHit.collider.CompareTag("Player"))
                            {
                                transform.localScale *= new Vector2(-1.0f, 1.0f);
                                UseAbility(0);
                            }
                        }
                    }
                }
            }
        }
    }


    public override void OnDeath()
    {
        base.OnDeath();
        UseAbility(Abilities.Length - 1);
    }
}
