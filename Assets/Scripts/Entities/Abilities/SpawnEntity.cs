using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEntity : Abilitiy
{
    public GameObject Spawn;
    public Vector3 SpawnLocation;

	// Use this for initialization
	protected override void Start ()
    {
        base.Start();
	}

    public override void CastAbility()
    {
        base.CastAbility();

        GameObject NewObject = Instantiate<GameObject>(Spawn);
        NewObject.transform.position = SpawnLocation;
    }
}
