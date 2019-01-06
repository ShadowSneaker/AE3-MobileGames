using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public UserInterface UI;

    internal InteractableObject Interact;

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
        if (Interact && !Interact.Active)
        {
            Interact.Interact();
            return;
        }

        if (OnGround())
        {
            base.Jump();
        }
        else
        {
            UseAbility(5);
        }
    }


    public void ActionRegistered(EDragDirection Action)
    {
        switch (Action)
        {
            case EDragDirection.Left:
                // If user is looking left.
                if (transform.localScale.x < 0.0f)
                {
                    if (OnGround())
                    {
                        UseAbility(1);
                    }
                }
                else
                {
                    UseAbility(3);
                }
                break;


            case EDragDirection.Right:
                // If user is looking Right.
                if (transform.localScale.x > 0.0f)
                {
                    if (OnGround())
                    {
                        UseAbility(1);
                    }
                }
                else
                {
                    UseAbility(3);
                }
                break;


            case EDragDirection.Up:
                UseAbility(2);
                break;


            case EDragDirection.Down:
                UseAbility(4);
                break;


            case EDragDirection.Tap:
                break;
        }
    }
}
