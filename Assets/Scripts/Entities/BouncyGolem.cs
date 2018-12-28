using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyGolem : MonoBehaviour
{
    private Entity Golem;
    private FireProjectile Project;

    public Vector2 LeftSpawnPos;
    public Vector2 RightSpawnPos;


	void Start ()
    {
        Golem = GetComponent<Entity>();
        Project = GetComponent<FireProjectile>();
	}
	

	void Update ()
    {
		if (Golem.OnGround())
        {
            Golem.Jump();
        }

        if (Golem.Abilities[0].GetAbilityUp)
        {
            
            if (Random.Range(0, 2) == 0)
            {
                // Spawn Left
                Project.Projectiles[0].SpawnOffset = LeftSpawnPos;
                //Project.Projectiles[0].RotationOffset = new Vector3(0.0f, 0.0f, 180.0f);
            }
            else
            {
                Project.Projectiles[0].SpawnOffset = RightSpawnPos;
                Project.Projectiles[0].RotationOffset = new Vector3(0.0f, 0.0f, 180.0f);
            }
            Golem.UseAbility(0);

        }
	}
}
