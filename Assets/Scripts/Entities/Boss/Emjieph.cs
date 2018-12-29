using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emjieph : MonoBehaviour
{
    public FireProjectile RagingSwipe;
    public Vector2 TopStartPos;

    public Vector2 BottomStartPos;

    public int DashDamage = 5;


    public float MinTime;
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
                Other.ApplyDamage(DashDamage);
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
            Debug.Log("Used ability 2");
            UsingAbility = 1;
            AbilityFinished(This.Abilities[1].Clip.length);
        }
        else if (Percent <= 30)
        {
            // Cast Winds of Change
            Debug.Log("Used ability 4");
            Force.Direction = ((Random.Range(0, 2) == 1) ? 1 : -1);
            UsingAbility = 3;
        }
        else if (Percent <= 50)
        {
            // Cast Furious Charge
            Debug.Log("Used ability 3");
            UsingAbility = 2;
        }
        else
        {
            // Cast Raging Swipe
            Debug.Log("Used ability 1");
            bool Up = (Random.Range(0, 2) == 0) ? false : true;

            RagingSwipe.Projectiles[0].SpawnOffset = (Up) ? TopStartPos : BottomStartPos;
            UsingAbility = 0;
            AbilityFinished(This.Abilities[0].Clip.length);
        }

        This.UseAbility(UsingAbility);
        
    }


    public void AbilityFinished(float AddedDelay)
    {
        StartCoroutine(StartCooldown(AddedDelay));
    }
}
