using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityTypes
{
    Normal,
    Undead
}

public class Entity : MonoBehaviour
{

    public struct EntitySounds
    {
        public AudioClip WalkSound;
        public AudioClip AttackSound;
        public AudioClip JumpSound;
        public AudioClip LandingSound;
        public AudioClip HealSound;
        public AudioClip ReviveSound;
        public AudioClip HurtSound;
        public AudioClip DeathSound;
    }




    /// Propperties

    // The type of entity this entity is.
    public EntityTypes Type;

    // The type of sounds this entity makes.
    public EntitySounds Sounds = new EntitySounds();

    // Should this entity be allowed to fly.
    public bool CanFly = false;

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

    // How far below the collider it will check to see if the character is on the ground.
    public float GroundOffset = 0.15f;
    
    // The list of abilities this Entity has.
    public Abilitiy[] Abilities = new Abilitiy[6];

    // Refernce to the UI Script
    //public UserInterface UI;

    // The minimum amount of items this entity can drop on death.
    public int MinDropCount;

    // The maximum amount of items this entity can drop on death.
    public int MaxDropCount;

    // Randomly selects X amount of objects to drop on death.
    public GameObject[] DropObjects;

    // The garenteed list of items this entity will drop.
    public GameObject[] WillDropItems;

    // A reference to the animator class for the Entity.
    protected Animator Anim;

    // A reference to the rigid body attached to the Entity.
    protected Rigidbody2D Rigid;



    // Determins if the entity is attacking or not.
    internal bool Attacking = false;



    // The default speed of the animation.
    private float AnimSpeed;

    // The current health the entity has.
    private int CurrentHealth;

    // The value determining if the entity is dead or alive.
    private bool Dead = false;

    // Prevents this entity from being damaged.
    private bool Immune = false;

    // Stores if the entity is flying.
    private bool Flying = false;

    // How large the Collider extents are for this entity (used for calculating if the entity is on the ground).
    private float DistanceToGround;

    // The distance between the left side and the right side of the collider extents.
    private float Offset;

    // A reference to the collider around the Entity
    private CapsuleCollider2D Col;

    private AudioManager SFX;


    /// TEMP VARIABLES DO NOT RELAY OF THESE VARIABLES THEY WILL BE REMOVED

    // Warning: Temporary Variable.
    public GameObject MELEE_COLIDER;



    /// Functions


    // Initializes all reference components.
    protected virtual void Start()
    {
        Rigid = GetComponent<Rigidbody2D>();

        SFX = GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioManager>();

        Anim = GetComponent<Animator>();
        AnimSpeed = Anim.speed;

        CurrentHealth = MaxHealth;

        Col = GetComponent<CapsuleCollider2D>();
        DistanceToGround = (Col.bounds.extents.y) + GroundOffset;
        Offset = Col.bounds.extents.x / 2;
        

        if (CanFly)
        {
            SetFlying(true);
        }
    }


    // Test Info and animations
    private void Update()
    {
        Anim.SetBool("Falling", Rigid.velocity.y < 0.0f);

        // Prevent Sliding

    }


    // Triggers when the object collides with another object.
    // Disables the falling animation.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (OnGround() && !Flying)
        {
            Anim.SetLayerWeight(1, 0);
            Anim.ResetTrigger("Jump");
            Anim.SetBool("Falling", false);
        }
    }


    // Damages a targed based off the inputted damage.
    // Kills the Entity if the health goes below 0.
    // If the Entity dies, spawn the held items.
    // @param Damage - The amount of damage this entity will be inflicted by.
    // @return - The total amount of damage this entity recieved.
    public int ApplyDamage(int Damage)
    {
        if (!Immune && !Dead)
        {
            CurrentHealth -= Damage;

            Anim.SetBool("Damaged", true);
            StartCoroutine(StartImmunityFrames());

            if (CurrentHealth <= 0)
            {
                Dead = true;
                Anim.SetBool("Dead", true);
                StartCoroutine(DropItems());
                Rigid.bodyType = RigidbodyType2D.Kinematic;
                Col.isTrigger = true;

                SFX.PlaySound(Sounds.DeathSound);
            }
            else
            {
                SFX.PlaySound(Sounds.HurtSound);
            }

            return Damage;
        }
        return 0;
    }


    // Heals a target based off the inputted amount.
    // This will damage Undead targets.
    // Revived Entities will not drop items.
    // The the entity is undead and being revived still heal the entity.
    // @param Amount - the value this entity should be healed.
    // @param CanRevive - Should this entity be revived if they are dead.
    // @return - The total amount of healing the unit recieved.
    public int ApplyHeal(int Amount, bool CanRevive = false)
    {
        if (!Dead || CanRevive)
        {
            int Total = 0;

            if (CanRevive && Dead)
            {
                Rigid.bodyType = RigidbodyType2D.Kinematic;
                Col.isTrigger = false;

                Dead = false;
                Anim.SetBool("Dead", false);
                DropObjects = new GameObject[0];
                WillDropItems = new GameObject[0];

                // Unlock controls.

                SFX.PlaySound(Sounds.ReviveSound);
                // Play Revive particle system.

                CurrentHealth += Total;
            }
            else
            {
                if (Type != EntityTypes.Undead)
                {
                    Total = Amount;
                    CurrentHealth += Total;
                    
                    SFX.PlaySound(Sounds.HealSound);
                }
                else
                {
                    ApplyDamage(Amount);
                    Total = 0;
                }
            }


            Mathf.Clamp(CurrentHealth, 0, MaxHealth);

            return Total;
        }
        return 0;
    }


    // The timer on how long the entity should be immune.
    public IEnumerator StartImmunityFrames()
    {
        Immune = true;
        yield return new WaitForSeconds(ImmunityFrameLength);

        Immune = false;        
    }


    // Drops a list of items around the entity.
    private IEnumerator DropItems()
    {
        for (int i = 0; i < WillDropItems.Length; ++i)
        {
            int Direction = Random.Range(-500, 500);
            int Height = Random.Range(500, 1000);
            GameObject SpawnedObject = Instantiate<GameObject>(WillDropItems[i], transform.position, transform.rotation);
            SpawnedObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Direction, Height));
            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 0; i < Random.Range(MinDropCount, MaxDropCount); ++i)
        {
            int Direction = Random.Range(-500, 500);
            int Height = Random.Range(500, 1000);

            GameObject SpawnedObject = Instantiate<GameObject>(DropObjects[Random.Range(0, DropObjects.Length)], transform.position, transform.rotation);
            SpawnedObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Direction, Height));
            yield return new WaitForSeconds(0.1f);
        }
    }


    // Casts an ability based off the index.
    // Puts the ability on cooldown when casted.
    // @param - What ability should be casted in the ability array
    public void UseAbility(int AbilityIndex)
    {
        if (Abilities[AbilityIndex])
        {
            Abilities[AbilityIndex].CastAbility();
        }
        else
        {
            Debug.Log("Ability " + AbilityIndex.ToString() + " is not set!");
        }
    }


    // Launches the entity in the air based on the JumpStrength.
    // The entity can only jump if they are on the ground.
    // Flying entities cannot jump.
    public virtual void Jump()
    {
        if (OnGround() && !Flying)
        {
            Rigid.velocity = new Vector3(Rigid.velocity.x, JumpStrength, 0.0f);

            Anim.SetTrigger("Jump");

            Anim.SetLayerWeight(1, 1);
        }
    }


    // Moves the entity left or right based off a value.
    // The value ranges from -1 to 1.
    // @param Value - The direction the chaeracter should move in (-1 to move left, 1 to move right).
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


    // Checks if the player is currently standing on a platform.
    // @return - Returns true if the entity is on the ground.
    public bool OnGround()
    {
        Vector2 StartPos = new Vector2(transform.position.x - Offset, transform.position.y - DistanceToGround);
        Vector2 EndPos = new Vector2(transform.position.x + Offset, transform.position.y - DistanceToGround);

        return Physics2D.Linecast(EndPos, StartPos);
    }


    // Checks if the entity has died.
    // @return - Returns if the entity is dead.
    public bool IsDead()
    {
        return Dead;
    }


    public void SetFlying(bool Fly)
    {
        Flying = Fly;

        //Rigid.simulated = !Fly;
        Rigid.gravityScale = (Fly) ? 0.0f : 1.0f;
    }


    public void FinishHurtAnim()
    {
        Anim.SetBool("Damaged", false);
    }


    public void PlayWalkSound(float Pitch)
    {
        //Sounds.WalkSound.pitch = Pitch;
        // Play Walk sound
    }


    public int GetHealth
    {
        get
        {
            return CurrentHealth;
        }
    }
}
