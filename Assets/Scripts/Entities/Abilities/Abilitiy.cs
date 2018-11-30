using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* The base information for an ability to be casted in the world.
 * Handles all the animations, timers and effects of the ability, and has overrideable functions for childeren to set the specific effects.
 * Note: This does not need to be placed on an entity but the game object should have an animator component attached.
 */
public class Abilitiy : MonoBehaviour {

    // How long the ability takes to be active again (Counts in seconds);
    public float Cooldown = 5f;

    // The animation clip that should be played when this ability is casted.
    public AnimationClip Clip;



    private bool AbilityUp = true;

    private Entity Owner;

    private Animator Anim;


	// Use this for initialization
	void Start ()
    {
        Owner = GetComponent<Entity>();
        Anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	}


    private IEnumerator StartCountdown()
    {
        AbilityUp = false;

        if (Owner)
        {
            Owner.Attacking = true;
        }

        yield return new WaitForSeconds(Cooldown);

        AbilityUp = true;

        if (Owner)
        {
            Owner.Attacking = true;
        }
    }


    // MAKE SURE YOU CALL THIS WHEN OVERRIDED!!! 
    public virtual void CastAbility()
    {
        if (AbilityUp)
        {
            
        }
    }


    public virtual void EndAbility()
    {
        StartCoroutine(StartCountdown());
    }
}
