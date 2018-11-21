using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EntityTypes
{
    Normal,
    Undead
}



public class Entity : MonoBehaviour {

    // The type of entity this entity is.
    public EntityTypes Type;

    // The total amount of health this entity starts with.
    public int Health = 3;

    // How fast this entity should move.
    public float MovementSpeed = 500.0f;

    // How long this entity should remain immune when damaged.
    public float ImmunityFrameLength = 0.5f;



    // A reference to the rigid body attached to the Entity.
    protected Rigidbody2D Rigid;


    private bool Dead = false;
    private bool Immune = true;


	// Use this for initialization
	protected virtual void Start ()
    {
        Rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    // Damages a targed based off the inputted damage.
    // Kills the Entity if the health goes below 0.
    // @param Target - The Entity that is being damaged.
    // @param Damage - The amount of damage this entity will be inflicted by.
    // @return - The total amount of damage this entity recieved.
    public static int ApplyDamage(Entity Target, int Damage)
    {
        if (!Target.IsImmune())
        {
            Target.Health -= Damage;

            // Play hurt animation
            // Play hurt sound
            // Lock controls.
            Target.StartImmunityFrames();

            if (Target.Health <= 0)
            {
                Target.Dead = true;
            }
            return Damage;
        }
        return 0;
    }


    // Heals a target based off the inputted amount.
    // This will damage Undead targets.
    // @param Target - The Entity that is being healed.
    // @param Amount - the value this entity should be healed.
    // @return - The total amount of healing the unit recieved.
    public static int ApplyHeal(Entity Target, int Amount)
    {
        int Total = Amount * ((Target.Type == EntityTypes.Undead) ? -1 : 1);
        Target.Health += Total;
        return Total;
    }


    public bool IsImmune()
    {
        return Immune;
    }


    public IEnumerator StartImmunityFrames()
    {
        yield return new WaitForSeconds(ImmunityFrameLength);

        Immune = true;
    }
}
