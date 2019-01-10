using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LadderScript : MonoBehaviour {

    public GameObject Up;
    public GameObject Down;

    public GameObject Player;

	void Start ()
    {
        Up.SetActive(false);
        Down.SetActive(false);
	}
	
	
	void Update () {
		
	}


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {

            Up.SetActive(true);
            Down.SetActive(true);
        }
       
        
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Up.SetActive(false);
        Down.SetActive(false);
    }



    public void OnClickUp()
    {
        
        Player.transform.localPosition = new Vector3(Player.transform.localPosition.x, Player.transform.localPosition.y + 40, Player.transform.localPosition.z);
    }

    public void OnClickDown()
    {
        Player.transform.localPosition = new Vector3(Player.transform.localPosition.x, Player.transform.localPosition.y - 40, Player.transform.localPosition.z);
    }

}

