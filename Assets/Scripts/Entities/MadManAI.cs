using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadManAI : MonoBehaviour
{
    //this is the script for the MadMan AI this will only run upon the enable of entity script

    //child object
    public GameObject ChildObject;

    // Get the Entity for the MadMan
    private Entity MadManEntity;

    //sprites for testing
    public Sprite Mad;

	void Start ()
    {
        MadManEntity = this.GetComponent<Entity>();
	}
	
	
	void Update ()
    {
		if(this.GetComponent<Entity>().enabled == true)
        {
            ChildObject.SetActive(false);
        }


        // do all the Attacking stuff when the sprite changes and he is alive
        if(this.GetComponent<SpriteRenderer>().sprite == Mad && !MadManEntity.IsDead())
        {
            
        }
	}
}
