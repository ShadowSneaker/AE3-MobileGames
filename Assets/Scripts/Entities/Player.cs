using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public UserInterface UI;

	// Use this for initialization
	protected override void Start ()
    {
        base.Start();

        if (UI)
        {
            UI.Init(GetHealth);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    public override void Jump()
    {
        base.Jump();
    }
}
