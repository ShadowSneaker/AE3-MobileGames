using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior_Basic : Abilitiy
{



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

        Owner.MELEE_COLIDER.SetActive(true);
    }

    public override void EndAbility()
    {
        base.EndAbility();

        Owner.MELEE_COLIDER.SetActive(false);
    }
}
