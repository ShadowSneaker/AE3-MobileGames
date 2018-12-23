using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : Abilitiy
{
    public Projectile SpawnProjectile;
    public float Lifetime = 5.0f;

    public float SpawnDelay;

    public float ArrowSpeed;

    public override void CastAbility()
    {
        base.CastAbility();
        StartCoroutine(Spawn());
    }


    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(SpawnDelay);
        Projectile Spawned = Instantiate<Projectile>(SpawnProjectile, transform.position, transform.rotation);
        Spawned.Speed = ArrowSpeed;
        Destroy(Spawned.gameObject, Lifetime);
    }
}
