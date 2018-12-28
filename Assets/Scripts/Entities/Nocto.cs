using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nocto : MonoBehaviour
{
    Transform Player;
    Entity Self;
    FireProjectile Fire;


	// Use this for initialization
	void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Self = GetComponent<Entity>();
        Fire = GetComponent<FireProjectile>();
	}
	

	void Update ()
    {
		if (Self.Abilities[0].GetAbilityUp)
        {
            // Find the location of the player and fire in that direction.
            Vector3 Diff = transform.position - Player.position;
            Diff.Normalize();

            float Rotation = Mathf.Atan2(Diff.y, Diff.x) * Mathf.Rad2Deg;

            Vector3 NewRotation = new Vector3(0.0f, 0.0f, Rotation);

            Fire.Projectiles[0].RotationOffset = NewRotation;
            Self.UseAbility(0);
        }
	}
}
