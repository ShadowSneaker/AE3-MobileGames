using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformation : Abilitiy
{
    public GameObject TransformObject;

    public Conveyor AttachToConveyor;


	// Use this for initialization
	protected override void Start ()
    {
        base.Start();
	}


    public override void CastAbility()
    {
        base.CastAbility();

        TransformObject.SetActive(true);

        if (AttachToConveyor)
        {
            AttachToConveyor.MoveObject = TransformObject.transform;
        }
        
        gameObject.SetActive(false);
    }
}
