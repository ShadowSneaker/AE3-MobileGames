using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour {

    //array of heart images
    private Image[] Hearts;

    // the heart image
    public Sprite Heart;


	void Start ()
    {
		
	}
	
	
	void Update ()
    {
		
	}

    // initialize the amount of hearts
    public void Init(int health)
    {
        Hearts = new Image[health];
    }

    public void HeartUpdate()
    {

    }

    public void DrawHearts()
    {

    }
}
