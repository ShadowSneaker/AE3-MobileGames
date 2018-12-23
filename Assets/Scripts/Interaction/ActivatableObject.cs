using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatableObject : MonoBehaviour
{
    public bool Activated;


	// Use this for initialization
	void Start ()
    {
        OnActivate();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    public void Activate()
    {
        Activated = !Activated;
        OnActivate();
    }


    public virtual void OnActivate()
    {

    }
}
