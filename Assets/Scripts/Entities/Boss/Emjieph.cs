﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emjieph : MonoBehaviour
{
    public FireProjectile RagingSwipe;
    public Vector2 TopStartPos;

    public Vector2 BottomStartPos;

    public int DashDamage = 5;


    public float MinTime = 4.0f;
    public float MaxTime = 8.0f;

    private float AnimationTime;

    private Entity This;
    private Wind Force;

    private int UsingAbility;


	// Use this for initialization
	void Start ()
    {
        This = GetComponent<Entity>();
        Force = GetComponent<Wind>();
        StartCoroutine(StartCooldown(0.0f));
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (This.GetHealth <= 20 && This.Type != EntityTypes.Undead)
        {
            This.Type = EntityTypes.Undead;
        }
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the boss is moving along the X axis.
        if (This.Rigid.velocity.x > 0.0f && This.Rigid.velocity.x < 0.0f)
        {
            Entity Other = collision.GetComponent<Entity>();
            if (Other)
            {
                Other.ApplyDamage(DashDamage, This);
            }
        }
    }


    private IEnumerator StartCooldown(float AddedDelay)
    {
        
        yield return new WaitForSeconds(Random.Range(MinTime, MaxTime) + AddedDelay);

        int Percent = Random.Range(0, 100);
        if (Percent <= 15)
        {
            // Cast Magma Bomb Drop
            UsingAbility = 1;
            AbilityFinished((This.Abilities[1].Clip) ? This.Abilities[0].Clip.length : 0.0f);
        }
        else if (Percent <= 30)
        {
            // Cast Winds of Change
            Force.Direction = ((Random.Range(0, 2) == 1) ? 1 : -1);
            UsingAbility = 3;
            AbilityFinished((This.Abilities[0].Clip) ? This.Abilities[3].Clip.length : 0.0f);
        }
        else if (Percent <= 50)
        {
            // Cast Furious Charge
            UsingAbility = 2;
            Force.Stop();
        }
        else
        {
            // Cast Raging Swipe
            bool Up = (Random.Range(0, 2) == 0) ? false : true;

            RagingSwipe.Projectiles[0].SpawnOffset = (Up) ? TopStartPos : BottomStartPos;
            UsingAbility = 0;
            
            AbilityFinished((This.Abilities[0].Clip) ? This.Abilities[0].Clip.length : 0.0f);
        }

        This.UseAbility(UsingAbility);
        
    }


    public void AbilityFinished(float AddedDelay)
    {
        StartCoroutine(StartCooldown(AddedDelay));
    }
}
