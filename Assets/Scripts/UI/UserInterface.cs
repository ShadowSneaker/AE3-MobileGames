using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour {
    


    //array of heart images
    public Image[] Hearts;

    // the heart image
    public Sprite HeartIMG;

    // health Bar for the UI
    public Transform ParentHealthBar;

    //array of healthslots
    HealthSlot[] Health;

	void Start ()
    {
        Health = ParentHealthBar.GetComponentsInChildren<HealthSlot>();
	}
	
	
	void Update ()
    {
		
	}

    public void HeartUpdate(int health)
    {
        //HeartUpdate function will be along the lines of this:
        for (int i = 0; i < Hearts.Length; ++i)
        {
         //If the player's current health is higher than the index, display the hearts. Otherwise disable them.
          Hearts[i].enabled = ((health - 1 >= i) ? true : false);
        }

        //for (int i = 0; i < Health.Length; i++)
        //{
        //    Hearts[i].enabled = false;
        //}
        //
        //for (int j = 0; j < health; j++)
        //{
        //    Hearts[j].enabled = true;
        //}
    }

    public void DrawHearts()
    {

    }

}
