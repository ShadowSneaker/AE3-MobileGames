using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* The base information for an ability to be casted in the world.
 * Handles all the animations, timers and effects of the ability, and has overrideable functions for childeren to set the specific effects.
 * Note: This does not need to be placed on an entity but the game object should have an animator component attached.
 */
public class Abilitiy : MonoBehaviour
{
    // Should this ability be locked until a specific level is reached?
    public bool Obtained = true;

    // The level this ability is unlocked.
    public int ObtainLevel;

    // Should this ability be on cooldown at the start of the game?
    public bool StartOnCooldown;

    // Should the ability have a random timer for the Cooldown - If disabled always use Cooldown.
    public bool RandomCoolDownTime;

    public float MinCooldown;

    // How long the ability takes to be active again (Counts in seconds);
    public float Cooldown = 5f;

    // The animation clip that should be played when this ability is casted.
    public AnimationClip Clip;

    // The sound this ability should play when attacking.
    public AudioClip SoundClip;

    public bool OverrideAnimation = true;


    // A reference to the entity that casted this ability.
    protected Entity Owner;



    // Determines if the ability is available.
    private bool AbilityUp = true;

    // The override controller, used to change the attacking animation clip to this ability's attacking animation clip.
    private AnimatorOverrideController AOC;

    // A reference to the animator used to play this ability's animation.
    protected Animator Anim;

    // The amount of time this ability's animation takes.
    private float AbilityTime;




	// Use this for initialization
	protected virtual void Start ()
    {
        Owner = GetComponent<Entity>();
        Anim = GetComponent<Animator>();

        AOC = new AnimatorOverrideController(Anim.runtimeAnimatorController);

        if (Anim && Clip)
        {
            AbilityTime = Clip.length;
        }

        if (StartOnCooldown)
        {
            StartCoroutine(StartCountdown());
        }
	}


    private IEnumerator AbilityDuration()
    {
        yield return new WaitForSeconds(AbilityTime);
        EndAbility();
    }


    protected IEnumerator StartCountdown()
    {
        AbilityUp = false;
        if (Owner)
        {
            Owner.Attacking = false;
        }
        
        float CooldownTime = (RandomCoolDownTime) ? Random.Range(MinCooldown, Cooldown) : Cooldown;
        yield return new WaitForSeconds(CooldownTime);

        AbilityUp = true;

    }


    // MAKE SURE YOU CALL THIS WHEN OVERRIDED!!! 
    public virtual void CastAbility()
    {
        if (AbilityUp)
        {
            AbilityUp = false;
            if (Owner)
            {
                Owner.Attacking = true;
            }

            if (Anim)
            {
                if (OverrideAnimation && Clip)
                {
                    var Anims = new List<KeyValuePair<AnimationClip, AnimationClip>>();

                    for (int i = 0; i < AOC.animationClips.Length; i++)
                    {
                        if (AOC.animationClips[i].name == "AttackAnim")
                        {
                            Anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(AOC.animationClips[i], Clip));
                            AOC.animationClips[i] = Clip;
                            break;
                        }
                    }
                    AOC.ApplyOverrides(Anims);
                    Anim.runtimeAnimatorController = AOC;
                    Anim.SetLayerWeight(2, 1.0f);
                    AOC.animationClips[0] = Clip;
                }

                Anim.SetBool("Attack", true);
            }

            if (SoundClip)
            {
                
            }

            StartCoroutine(AbilityDuration());
        }
    }


    public virtual void EndAbility()
    {
        StartCoroutine(StartCountdown());
        
        Anim.SetBool("Attack", false);
        Anim.SetLayerWeight(2, 0.0f);
    }


    public bool GetAbilityUp
    {
        get
        {
            return AbilityUp;
        }
    }
}
