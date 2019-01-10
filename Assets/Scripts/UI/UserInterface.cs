using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour {
    // final thing to be done is to decide weher the health update goes


    //array of heart images
    private Image[] Hearts;

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

    // initialize the amount of hearts
    public void Init(int Health)
    {
        Hearts = new Image[Health];

        for (int i = 0; i < Hearts.Length; i++)
        {
            Hearts[i].sprite = HeartIMG;
        }
    }

    public void HeartUpdate(int health)
    {
        for(int i = 0; i < Hearts.Length; i++)
        {
            Health[i].AddHeart(HeartIMG);
        }
    }

    public void DrawHearts()
    {

    }

}
