using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBlock : Abilitiy
{
    public BoxCollider2D BlockZone;
    public Abilitiy DisableAbility;

    private bool Active;


    public override void CastAbility()
    {
        if (GetAbilityUp)
        {
            base.CastAbility();
            Active = !Active;

            if (Active)
            {
                if (BlockZone)
                {
                    BlockZone.enabled = true;
                }

                DisableAbility.enabled = false;
            }
            else
            {
                EndAbility();
            }
        }
    }


    public override void EndAbility()
    {
        base.EndAbility();
        if (BlockZone)
        {
            BlockZone.enabled = false;
        }

        DisableAbility.enabled = true;
    }
}
