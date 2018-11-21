using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : Entity
{

    private Joystick StickInput;

    public Abilitiy[] Abilities = new Abilitiy[6];


	// Use this for initialization
	protected override void Start ()
    {
        base.Start();
	}

	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Q))
        {
            UseAbility(0);
        }

        

        // Player Movement.
        Vector2 MoveVec = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), Rigid.velocity.y);

        if (MoveVec.magnitude > 1)
        {
            MoveVec = MoveVec.normalized;
        }

        Rigid.velocity = (MoveVec * MovementSpeed) * Time.deltaTime;
	}


    void UseAbility(int AbilityIndex)
    {
        if (Abilities[AbilityIndex])
        {
            Abilities[AbilityIndex].CastAbility();
        }
        else
        {
            Debug.Log("Ability: " + AbilityIndex.ToString() + " Is not set!");
        }
    }


    public void Jump()
    {
        Rigid.velocity = new Vector2(Rigid.velocity.x, 10.0f);
    }

    public void StopJumping()
    {

    }
}
