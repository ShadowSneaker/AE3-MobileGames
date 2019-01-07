using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EntityTypes
{
    Normal,
    Undead,
    Object
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

    // The amount of damage other entities take when they collide with this object.
    public int ContactDamage;

    // How high the entity will jump.
    public float JumpStrength = 10.0f;

    // How long this entity should remain immune when damaged.
    public float ImmunityFrameLength = 0.5f;

    // The downward velocity the player needs to reach in order to die on impact.
    public float TerminalVelocity = -20.0f;

    // How far below the collider it will check to see if the character is on the ground.
    public float GroundOffset = 0.15f;
    
    // The list of abilities this Entity has.
    public Abilitiy[] Abilities;

    // Refernce to the UI Script
    //public UserInterface UI;

    // The minimum amount of items this entity can drop on death.
    public int MinDropCount;

    // The maximum amount of items this entity can drop on death.
    public int MaxDropCount;
 


    // The base item object that should be spawned.
    public ItemPickup DropItemPrefab;

    // Randomly selects X amount of objects to drop on death.
    public ItemScript[] DropObjects;

    // The garenteed list of items this entity will drop.
    public ItemScript[] WillDropItems;



    // A reference to the animator class for the Entity.
    protected Animator Anim;

    // The distance between the left side and the right side of the collider extents.
    protected float Offset;

    
    // A reference to the rigid body attached to the Entity.
    internal Rigidbody2D Rigid;

    // Determins if the entity is attacking or not.
    internal bool Attacking = false;

    internal bool Reflect;



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

    // Determines if the unit can do anything.
    private bool Stunned;


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
        if (Col)
        {
            DistanceToGround = (Col.bounds.extents.y) + GroundOffset;
            Offset = Col.bounds.extents.x / 2;
        }


        if (CanFly)
        {
            SetFlying(true);
        }


        // Enable abilities.
        int CurrentRoom = SceneManager.GetActiveScene().buildIndex;
        for (int i = 0; i < Abilities.Length; ++i)
        {
            if (Abilities[i])
            {
                if (!Abilities[i].Obtained && CurrentRoom >= Abilities[i].ObtainLevel)
                {
                    Abilities[i].Obtained = true;
                }
            }
        }
    }


    // Test Info and animations
    private void Update()
    {
        if (Type != EntityTypes.Object)
        {
            Anim.SetBool("Falling", Rigid.velocity.y < 0.0f);
        }
        // Prevent Sliding

    }


    // Triggers when the object collides with another object.
    // Disables the falling animation.
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (!Dead)
        {
            if (OnGround() && !Flying)
            {
                Anim.SetLayerWeight(1, 0);
                Anim.ResetTrigger("Jump");
                Anim.SetBool("Falling", false);
            }

            Entity Other = collision.gameObject.GetComponent<Entity>();
            if (Other)
            {
                Other.ApplyDamage(ContactDamage, this);
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Floor"))
            {
                Col.isTrigger = true;

                Rigid.bodyType = RigidbodyType2D.Kinematic;
                Rigid.velocity = Vector2.zero;
            }
        }
    }


    // Triggers when the object overlaps with another object.
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Dead)
        {
            Entity Other = collision.GetComponent<Entity>();
            if (Other)
            {
                Other.ApplyDamage(ContactDamage, this);
            }
        }
    }


    // Damages a targed based off the inputted damage.
    // Kills the Entity if the health goes below 0.
    // If the Entity dies, spawn the held items.
    // @param Damage - The amount of damage this entity will be inflicted by.
    // @return - The total amount of damage this entity recieved.
    public int ApplyDamage(int Damage, Entity Attacker)
    {
        if (!Reflect || !Attacker)
        {
            if (!Immune && !Dead && Damage > 0)
            {
                CurrentHealth -= Damage;

                Anim.SetBool("Damaged", true);
                StartCoroutine(StartImmunityFrames());
                if (CurrentHealth <= 0)
                {
                    Dead = true;
                    Anim.SetBool("Dead", true);
                    StartCoroutine(DropItems());

                    if (Anim)
                    {
                        Anim.SetLayerWeight(1, 0);
                        Anim.ResetTrigger("Jump");
                        Anim.SetBool("Falling", false);
                    }

                    SFX.PlaySound(Sounds.DeathSound);
                    OnDeath();
                }
                else
                {
                    SFX.PlaySound(Sounds.HurtSound);
                }

                return Damage;
            }
        }
        else
        {
            if (Attacker)
                Attacker.ApplyDamage(Damage, this);
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
                if (Rigid)
                {
                    Rigid.bodyType = RigidbodyType2D.Kinematic;
                }

                if (Col)
                {
                    Col.isTrigger = false;
                }

                Dead = false;
                Anim.SetBool("Dead", false);
                DropObjects = new ItemScript[0];
                WillDropItems = new ItemScript[0];

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
                    ApplyDamage(Amount, this);
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

            ItemPickup SpawnedObject = Instantiate<ItemPickup>(DropItemPrefab, transform.position, transform.rotation);
            SpawnedObject.Item = WillDropItems[i];
            SpawnedObject.GetComponent<SpriteRenderer>().sprite = SpawnedObject.Item.Image;

            SpawnedObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Direction, Height));
            yield return new WaitForSeconds(0.1f);
        }
        

        for (int i = 0; i < Random.Range(MinDropCount, MaxDropCount); i++)
        {
            int Direction = Random.Range(-500, 500);
            int Height = Random.Range(500, 1000);

            ItemPickup SpawnedObject = Instantiate<ItemPickup>(DropItemPrefab, transform.position, transform.rotation);
            SpawnedObject.Item = DropObjects[Random.Range(0, DropObjects.Length)];
            SpawnedObject.GetComponent<SpriteRenderer>().sprite = SpawnedObject.Item.Image;

            SpawnedObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Direction, Height));
            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator StartStunned(float Duration)
    {
        Stunned = true;
        yield return new WaitForSeconds(Duration);
        Stunned = false;
    }

    // Called when this entity dies.
    public virtual void OnDeath()
    {}


    // Casts an ability based off the index.
    // Puts the ability on cooldown when casted.
    // @param - What ability should be casted in the ability array
    public void UseAbility(int AbilityIndex)
    {
        if (!Stunned && Abilities[AbilityIndex] && !Attacking)
        {
            if (Abilities[AbilityIndex].Obtained)
            {
                if (Abilities[AbilityIndex])
                {
                    Abilities[AbilityIndex].CastAbility();
                    Debug.Log("Used ability: " + AbilityIndex.ToString());
                }
                else
                {
                    Debug.Log("Ability " + AbilityIndex.ToString() + " is not set!");
                }
            }
        }
    }


    // Launches the entity in the air based on the JumpStrength.
    // The entity can only jump if they are on the ground.
    // Flying entities cannot jump.
    public virtual void Jump()
    {
        if (!Stunned)
        {
            if (OnGround() && !Flying && Rigid)
            {
                Rigid.velocity = new Vector3(Rigid.velocity.x, JumpStrength, 0.0f);

                Anim.SetTrigger("Jump");

                Anim.SetLayerWeight(1, 1);
            }
        }
    }


    // Moves the entity left or right based off a value.
    // The value ranges from -1 to 1.
    // Problem if forced in a direciton this will lock the X movements.
    // @param Value - The direction the chaeracter should move in (-1 to move left, 1 to move right).
    public void MoveSideways(float Value)
    {
        if (!Stunned)
        {
            if (!Attacking && Rigid)
            {
                Rigid.velocity = new Vector2((Value * MovementSpeed) * Time.deltaTime, Rigid.velocity.y);
                float Normalised = Value * ((Value >= 0.0f) ? 1 : -1);

                if (Value > 0.0f && transform.localScale.x != 1.0f)
                {
                    transform.localScale = new Vector2(1.0f, transform.localScale.y);
                }
                else if (Value < 0.0f && transform.localScale.x != -1.0f)
                {
                    transform.localScale = new Vector2(-1.0f, transform.localScale.y);
                }

                Anim.SetFloat("MovementSpeed", Normalised);

                if (Normalised > 0.01f)
                {
                    Anim.speed = AnimSpeed * Normalised;
                }
            }
        }
        //else
        //{
        //    Rigid.velocity = new Vector2(0.0f, Rigid.velocity.y);
        //}
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
