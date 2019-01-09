using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Entity
{
    public UserInterface UI;

    internal InteractableObject Interact;

    private TransitionScript Transition;

    private bool Charged;




	// Use this for initialization
	protected override void Start ()
    {
        base.Start();

        if (UI)
        {
            UI.Init(GetHealth);
        }

        Transition = FindObjectOfType<TransitionScript>();
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


    private IEnumerator Charge()
    {
        yield return new WaitForSeconds(3.0f);
        Charged = true;
    }


    public void AttackReleased()
    {
        Attacking = false;
        if (Charged && Abilities[6].GetAbilityUp)
        {
            Debug.Log("Ran Charged");
            UseAbility(6);
        }
        else
        {
            Debug.Log("Ran Basic");
            UseAbility(0);
            StopCoroutine(Charge());
        }
        Charged = false;
    }


    public void AttackPressed()
    {
        StartCoroutine(Charge());
        Attacking = true;
    }


    public override void OnDeath()
    {
        StartCoroutine(DeathTimer());
    }


    private IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(1.5f);

        if (Transition)
        {
            Transition.ReloadScene();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        }
    }
}
