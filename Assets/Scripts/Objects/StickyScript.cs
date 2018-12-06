using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A helpful script used to parent an object to this object
// so they can move together (used primaraly with moving platforms).
public class StickyScript : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Entity>())
        {
            collision.transform.parent = transform;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Entity>())
        {
            collision.transform.parent = null;
        }
    }
}
