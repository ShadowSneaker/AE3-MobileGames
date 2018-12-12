using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour {

    //array of heart images
    private Image[] Hearts;

    // the heart image
    public Sprite HeartIMG;


	void Start ()
    {
		
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

    public void HeartUpdate()
    {

    }

    public void DrawHearts()
    {

    }
}
