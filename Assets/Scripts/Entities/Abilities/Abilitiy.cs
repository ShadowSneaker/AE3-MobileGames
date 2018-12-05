﻿using System.Collections;
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

    private AnimatorOverrideController AOC;

    protected bool AbilityUp = true;

    private Entity Owner;

    private Animator Anim;

    private string AttackName = "AttackAnim";

    bool Test = false;

    float AbilityTime = 0.0f;


	// Use this for initialization
	protected virtual void Start ()
    {
        Owner = GetComponent<Entity>();
        Anim = GetComponent<Animator>();

        AOC = new AnimatorOverrideController(Anim.runtimeAnimatorController);

        AbilityTime = Clip.length;
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    private IEnumerator AbilityDuration()
    {
        yield return new WaitForSeconds(AbilityTime);
        EndAbility();
    }


    private IEnumerator StartCountdown()
    {
        AbilityUp = false;

        

        yield return new WaitForSeconds(Cooldown);

        AbilityUp = true;

        if (Owner)
        {
            Owner.Attacking = false;
        }
    }


    // MAKE SURE YOU CALL THIS WHEN OVERRIDED!!! 
    public virtual void CastAbility()
    {
        if (AbilityUp)
        {

            //Test = true;
            if (Owner)
            {
                Owner.Attacking = true;
            }

            var Anims = new List<KeyValuePair<AnimationClip, AnimationClip>>();

            for (int i = 0; i < AOC.animationClips.Length; i++)
            {
                if (AOC.animationClips[i].name == "AttackAnim")
                {
                    Anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(AOC.animationClips[i], Clip));
                    AOC.animationClips[i] = Clip;
                    //AttackName = Clip.name;
                    break;
                }
            }

            AOC.ApplyOverrides(Anims);
            Anim.runtimeAnimatorController = AOC;


            Anim.SetLayerWeight(2, 1.0f);
            Anim.SetBool("Attack", true);

            
            AOC.animationClips[0] = Clip;

            StartCoroutine(AbilityDuration());
        }
    }


    public virtual void EndAbility()
    {
        //if (Test)
        //{
            //Test = false;
            // Anim for some reason keeps loosing reference and thus must be set here.
            Anim = GetComponent<Animator>();

            StartCoroutine(StartCountdown());
            Anim.SetBool("Attack", false);
            Anim.SetLayerWeight(2, 0.0f);
        //}
    }
}
