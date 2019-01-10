using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeBoulder : MonoBehaviour {

    private Inventory PlayerInventory;

    bool DynamiteIn;
    bool torchIn;

    public GameObject boulder;


	void Start ()
    {
        PlayerInventory = FindObjectOfType<Inventory>();
	}
	
	
	void Update ()
    {
		if(DynamiteIn && torchIn)
        {
            boulder.SetActive(false);
        }
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        for(int i = 0; i < PlayerInventory.Items.Count; i++)
        {
            if(PlayerInventory.Items[i].ItemName == "Dynamite")
            {
                DynamiteIn = true;
            }

            if(PlayerInventory.Items[i].ItemName == "Torch")
            {
                torchIn = true;
            }

        }
    }

}
