﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EntityTypes
{
    Normal,
    Undead
}


public class Entity : MonoBehaviour
{

    /// Propperties

    // The type of entity this entity is.
    public EntityTypes Type;

    // The total amount of health this entity starts with.
    public int MaxHealth = 3;

    // How fast this entity should move.
    public float MovementSpeed = 500.0f;

    // How high the entity will jump.
    public float JumpStrength = 10.0f;

    // How long this entity should remain immune when damaged.
    public float ImmunityFrameLength = 0.5f;

    // The downward velocity the player needs to reach in order to die on impact.
    public float TerminalVelocity = -20.0f;

    // The list of abilities this Entity has.
    public Abilitiy[] Abilities = new Abilitiy[6];

    // How far below the collider it will check to see if the character is on the ground.
    public float GroundOffset = 0.15f;


    // A reference to the animator class for the Entity.
    protected Animator Anim;

    // A reference to the rigid body attached to the Entity.
    protected Rigidbody2D Rigid;
    
    

    
    // The default speed of the animation.
    private float AnimSpeed;

    // The current health the entity has.
    private int CurrentHealth;

    // The value determining if the entity is dead or alive.
    private bool Dead = false;

    // Prevents this entity from being damaged.
    private bool Immune = false;

    // How large the Collider extents are for this entity (used for calculating if the entity is on the ground).
    private float DistanceToGround;

    private float Offset;

    private CapsuleCollider2D Col;



    
    public LineRenderer DebugLine;


    /// Functions


    // Use this for initialization
    protected virtual void Start ()
    {
        Rigid = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        AnimSpeed = Anim.speed;

        CurrentHealth = MaxHealth;

        Col = GetComponent<CapsuleCollider2D>();
        DistanceToGround = (Col.bounds.extents.y - Col.offset.y) + GroundOffset;
        Offset = Col.bounds.extents.x + Col.offset.x;


    }


    // Damages a targed based off the inputted damage.
    // Kills the Entity if the health goes below 0.
    // @param Target - The Entity that is being damaged.
    // @param Damage - The amount of damage this entity will be inflicted by.
    // @return - The total amount of damage this entity recieved.
    public int ApplyDamage(int Damage)
    {
        if (!Immune && !Dead)
        {
            CurrentHealth -= Damage;

            Anim.SetBool("Damaged", true);
            // Play hurt sound
            // Lock controls.
            StartCoroutine(StartImmunityFrames());

            if (CurrentHealth <= 0)
            {
                Dead = true;
            }


            Debug.Log(CurrentHealth);
            return Damage;
        }
        return 0;
    }


    // Heals a target based off the inputted amount.
    // This will damage Undead targets.
    // @param Target - The Entity that is being healed.
    // @param Amount - the value this entity should be healed.
    // @return - The total amount of healing the unit recieved.
    public int ApplyHeal(int Amount)
    {
        int Total = Amount * ((Type == EntityTypes.Undead) ? -1 : 1);
        CurrentHealth += Total;


        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }

        return Total;
    }


    // The timer on how long the entity should be immune.
    public IEnumerator StartImmunityFrames()
    {
        Immune = true;
        yield return new WaitForSeconds(ImmunityFrameLength);

        Immune = false;
        Anim.SetBool("Damaged", false);
    }


    // Casts an ability based off the index.
    // Puts the ability on cooldown when casted.
    // @param - 
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
        if (OnGround())
        {
            Rigid.velocity = new Vector2(Rigid.velocity.x, JumpStrength);
            Anim.SetTrigger("Jump");
        }
    }


    public void MoveSideways(float Value)
    {
        Rigid.velocity = new Vector2(Value * MovementSpeed * Time.deltaTime, Rigid.velocity.y);

        float Num = Value * ((Value >= 0.0f) ? 1 : -1);

        if (Value > 0.0f)
        {
            transform.localScale = new Vector2(1.0f, transform.localScale.y);
        }
        else if (Value < 0.0f)
        {
            transform.localScale = new Vector2(-1.0f, transform.localScale.y);
        }

        Anim.SetFloat("MovementSpeed", Num);

        if (Value > 0.0f)
        {
            Anim.speed = AnimSpeed * Value * ((Value > 0.0f) ? 1 : -1);
        }
    }


    public bool OnGround()
    {
        Vector2 StartPos = new Vector2(transform.position.x - Offset, transform.position.y - DistanceToGround);
        Vector2 EndPos   = new Vector2(transform.position.x + Offset, transform.position.y - DistanceToGround);

        return Physics2D.Linecast(EndPos, StartPos);
    }


    public bool IsDead()
    {
        return Dead;
    }


    private void LayerChange()
    {
        // using the grounded method to test layer and test attacking
        if(!OnGround())
        {
            Anim.SetLayerWeight(1, 1);
        }
        else
        {
            Anim.SetLayerWeight(1, 0);
        }

        // this will be where i add the layer change for attacking
        // ability parameters will be added later in animator

    }


    


}
