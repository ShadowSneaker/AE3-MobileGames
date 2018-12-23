using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartTrap : ActivatableObject
{
    public Abilitiy ArrowAbility;


    public override void OnActivate()
    {
        ArrowAbility.CastAbility();
    }
}
