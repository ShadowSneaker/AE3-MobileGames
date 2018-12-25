using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartTrap : ActivatableObject
{
    public Abilitiy ArrowAbility;


    public override void OnActivate()
    {
        if (Activated)
        {
            Activated = false;
            ArrowAbility.CastAbility();
        }
    }
}
