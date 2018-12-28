using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : Entity
{
    public bool AlwaysAttack;
    public int AlwaysAttackAbility;

    private bool EnableAlways;

    public bool SearchBehind = true;
    public float RaycastLength = Mathf.Infinity;
    public int RaycastAttackAbility;

    // Should it disable Always attack when Raycast hits the player?
    public bool CancleAlwaysWhenRaycastHit;


	// Use this for initialization
	protected override void Start ()
    {
        base.Start();


	}


    private void Update()
    {
        if (Abilities[AlwaysAttackAbility].GetAbilityUp)
        {
            if (AlwaysAttack && EnableAlways)
            {
                UseAbility(0);
            }
        }

        if (CancleAlwaysWhenRaycastHit)
        {
            if (Abilities[RaycastAttackAbility].GetAbilityUp)
            {
                RaycastHit2D Hit = Physics2D.Raycast(transform.position + (new Vector3(Offset + 0.2f, 0.0f, 0.0f) * transform.localScale.x), Vector2.right * transform.localScale.x, RaycastLength);
                if (Hit)
                {
                    if (Hit.collider.CompareTag("Player"))
                    {
                        UseAbility(RaycastAttackAbility);
                        EnableAlways = false;
                    }
                    else if (SearchBehind)
                    {
                        RaycastHit2D BehindHit = Physics2D.Raycast(transform.position + (new Vector3(Offset + 0.4f, 0.0f, 0.0f) * -transform.localScale.x), Vector2.left * transform.localScale);
                        if (BehindHit)
                        {
                            if (BehindHit.collider.CompareTag("Player"))
                            {
                                transform.localScale *= new Vector2(-1.0f, 1.0f);
                                UseAbility(RaycastAttackAbility);
                                EnableAlways = false;
                            }
                        }
                        else
                        {
                            EnableAlways = true;
                        }
                    }
                    else
                    {
                        EnableAlways = true;
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
