using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedMushroom : Entity
{
    public int Damage = 9999;
    public float Range = 5.0f;

    public ParticleSystem Particles;
	// Use this for initialization
	protected override void Start ()
    {
        base.Start();
	}


    public override void OnDeath()
    {
        Particles.Play();

        Entity[] Entities = FindObjectsOfType<Entity>();

        for (int i = 0; i < Entities.Length; ++i)
        {
            float Distance = Vector3.Distance(Entities[i].transform.position, transform.position);

            if (Distance <= Range)
            {
                Entities[i].ApplyDamage(Damage);
            }
        }
    }
}
