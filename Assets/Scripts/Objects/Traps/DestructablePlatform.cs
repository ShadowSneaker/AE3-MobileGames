using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructablePlatform : MonoBehaviour
{
    private Animator Anim;


	// Use this for initialization
	void Start ()
    {
        Anim = GetComponent<Animator>();
	}


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Entity Other = collision.gameObject.GetComponent<Entity>();
        if (Other)
        {
            Anim.SetBool("Play", true);
        }
    }


    protected virtual void Destroy()
    {
        Debug.Log("Ran");
        gameObject.SetActive(false);
    }
}
