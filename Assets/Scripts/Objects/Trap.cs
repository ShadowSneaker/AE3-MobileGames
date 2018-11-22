using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    // How much damage this trap should inflict.
    public int Damage;

    // How long the delay between applying damage.
    public float TickTime = 1.0f;

    private List<Entity> DamageEntities = new List<Entity>();

	
	// Damages all entities touching this trap.
	void Update ()
    {
        if (DamageEntities.Count > 0)
        {
            for (int i = 0; i < DamageEntities.Count; ++i)
            {
                DamageEntities[i].ApplyDamage(Damage);

                if (DamageEntities[i].IsDead())
                {
                    DamageEntities.RemoveAt(i);
                }
            }
        }
	}


    // Adds the object when it enters the collider.
    private void OnCollisionEnter2D(Collision2D collision)
    { 
       
        Entity Obj = collision.gameObject.GetComponent<Entity>();
        
        if (Obj)
        {
            DamageEntities.Add(Obj);
        }
    }


    // Removes the object when it leaves the collider.
    private void OnCollisionExit2D(Collision2D collision)
    {
        Entity Obj = collision.gameObject.GetComponent<Entity>();

        if (Obj)
        {
            DamageEntities.Remove(Obj);
        }
    }
}
