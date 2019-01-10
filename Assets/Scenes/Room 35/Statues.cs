using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statues : MonoBehaviour {

    
    public GameObject Statue;
    public GameObject StatueGreen;

    public DoorScript Door;

    void Start ()
    {
		
	}
	
	
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Door.Activate();

        Statue.SetActive(false);
        StatueGreen.SetActive(true);

        
    }
}
