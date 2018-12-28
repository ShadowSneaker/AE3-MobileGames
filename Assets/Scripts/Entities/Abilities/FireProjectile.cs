using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpawnParams
{
    // The projectile that should be spawned.
    public Projectile SpawnProjectile;

    // Should the projectile's speed be changed to a specified speed?
    public bool OverrideProjectileSpeed;

    // If OerrideProjectileSpeed is enabled this is the speed the projectile will move at.
    public float ProjectileSpeed;

    // Should the projectile be spawned with the same rotation as this class?
    public bool UseParentRotation;

    // The added rotation applied to the projectile after spawning.
    public Vector3 RotationOffset;

    // Should the projectile be spawned at the same location as this class?
    public bool UseParentPosition;

    // The added position applied to the projectile after spawning.
    public Vector3 SpawnOffset;

    // How long the projectile will exist (in seconds).
    public float Lifetime;

    // Determines how long it will wait before firing a projectile.
    public float SpawnDelay;

    // Should the projectile apply damage based off a range.
    public bool UseDamageRange;

    // The minimum damage the projectile will apply.
    public int MinDamage;

    // The maximum damage the projectile will apply.
    public int MaxDamage;
}



public class FireProjectile : Abilitiy
{
    // A list of all projectile and their settings that should be fired.
    public SpawnParams[] Projectiles;

    // How long the object should wait before firing the next projectile index.
    public float DelayBetweenProjectiles;
    


    public override void CastAbility()
    {
        base.CastAbility();
        StartCoroutine(Spawn());
    }


    private IEnumerator Spawn()
    {
        for (int i = 0; i < Projectiles.Length; ++i)
        {
            yield return new WaitForSeconds(Projectiles[i].SpawnDelay + DelayBetweenProjectiles);

            Projectile Spawned = Instantiate<Projectile>(Projectiles[i].SpawnProjectile);

            // Override Position
            if (Projectiles[i].UseParentPosition)
            {
                Spawned.transform.position = transform.position + Projectiles[i].SpawnOffset;
            }
            else
            {
                Spawned.transform.position = Projectiles[i].SpawnOffset;
            }


            // Override Rotation
            if (Projectiles[i].UseParentRotation)
            {
                Spawned.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + Projectiles[i].RotationOffset);
            }
            else
            {
                Spawned.transform.rotation = Quaternion.Euler(Spawned.transform.rotation.eulerAngles + Projectiles[i].RotationOffset);
            }


            if (Projectiles[i].UseDamageRange)
            {
                Spawned.Damage = Random.Range(Projectiles[i].MinDamage, Projectiles[i].MaxDamage);
            }


            // Override Speed
            Spawned.Speed = (Projectiles[i].OverrideProjectileSpeed) ? Projectiles[i].ProjectileSpeed : Spawned.Speed;


            //Spawned.OverrideSpeed = Projectiles[i].OverrideProjectileSpeed;
            Spawned.Reverse = (transform.localScale.x < 0.0f);
            Spawned.Owner = gameObject;
            Destroy(Spawned.gameObject, Projectiles[i].Lifetime);
        }
    }
}
