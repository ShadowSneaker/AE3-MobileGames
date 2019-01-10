using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : InteractableObject
{
    Animator Anim;


    private void Start()
    {
        Anim = GetComponent<Animator>();
    }


    public override void Interact()
    {
        base.Interact();
        // Anim.SetBool("Play", true);
    }


    public void OnTriggerEnter2D(Collider2D Collision)
    {
        Player OBJ = Collision.GetComponent<Player>();
        if (OBJ && !OBJ.Interact)
        {
            OBJ.Interact = this;
        }
    }


    public void OnTriggerExit2D(Collider2D Collision)
    {
        Player OBJ = Collision.GetComponent<Player>();
        if (OBJ && OBJ.Interact == this)
        {
            OBJ.Interact = null;
        }
    }
}
