using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior_Basic : Abilitiy
{
    public bool UseDamageRange = false;
    public int Damage = 1;

    public int MaxDamage = 3;

	// Use this for initialization
	protected override void Start ()
    {
        base.Start();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    public override void CastAbility()
    {
        base.CastAbility();

        Owner.MELEE_COLIDER.GetComponent<BoxCollider2D>().enabled = true;

    }

    public override void EndAbility()
    {
        base.EndAbility();

        Owner.MELEE_COLIDER.GetComponent<BoxCollider2D>().enabled = false;
    }
}
