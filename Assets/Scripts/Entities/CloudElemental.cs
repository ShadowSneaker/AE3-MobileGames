using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudElemental : Entity
{
    //public int ContactDamage = 1;
    public float AbilityDelay = 5.0f;


    public float FireRange = 20.0f;

    public DoorScript ExitDoor;

    
    private FireProjectile Abil;


    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        Abil = GetComponent<FireProjectile>();
    }


    private void LateUpdate()
    {
        if (Abilities[0].GetAbilityUp)
        {
            Abil.Projectiles[0].RotationOffset = new Vector3(Abil.Projectiles[0].RotationOffset.x, Abil.Projectiles[0].RotationOffset.y, Random.Range(-FireRange, FireRange));
            Abil.Projectiles[1].RotationOffset = new Vector3(Abil.Projectiles[0].RotationOffset.x, Abil.Projectiles[0].RotationOffset.y, Random.Range(-FireRange, FireRange));
            UseAbility(0);
        }
    }


    public override void OnDeath()
    {
        ExitDoor.Activate();
        Destroy(gameObject);
    }
}
