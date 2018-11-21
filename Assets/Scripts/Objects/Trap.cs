using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{

    public int Damage;



	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionStay2D(Collision2D collision)
    { 
       
        Entity Obj = collision.gameObject.GetComponent<Entity>();
        
        if (Obj)
        {
            Obj.ApplyDamage(Damage);
        }
    }
}
