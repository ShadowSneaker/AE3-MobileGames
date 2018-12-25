using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMelee : MonoBehaviour {

    int Damage = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity Hit = collision.GetComponent<Entity>();
        if (Hit && !collision.CompareTag("Player"))
        {
            Hit.ApplyDamage(Damage);
        }
    }
}
