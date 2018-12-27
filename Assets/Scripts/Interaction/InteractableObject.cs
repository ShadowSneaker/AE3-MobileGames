using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public bool Active;

    public UnityEvent OnInteract;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public virtual void Interact()
    {
        if (OnInteract != null)
        {
            OnInteract.Invoke();
        }
    }
}
