using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public float Strength = 25.0f;

    private Animator Anim;

    private Entity Hit;



	// Use this for initialization
	void Start ()
    {
        Anim = GetComponent<Animator>();
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        Hit = collision.gameObject.GetComponent<Entity>();

        if (Hit)
        {
            Anim.SetBool("Play", true);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        
        Hit = null;
    }

    // Fix player movement so that the MoveSideways function isn't called every frame.
    public void AddForce()
    {
        if (Hit)
        {
            Hit.GetComponent<Rigidbody2D>().velocity = (transform.up * Strength);
        }
    }


    public void ResetAnim()
    {
        Anim.SetBool("Play", false);
    }
}
