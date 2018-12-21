using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRange : MonoBehaviour
{
    private DoorScript Parent;



    private void Start()
    {
        Parent = transform.parent.GetComponent<DoorScript>();
    }


    private void OnTriggerEnter2D(Collider2D Collision)
    {
        if (Collision.CompareTag("Player"))
        {
            //for (int i = 0; i < Inventory.Length; ++i)
            //{
            //  if (Invetory[i] == UnlockItem)
            //  {
            //      Inventory[i].Remove();
            //      Parent.Activate();
            //  }
            //}
        }
    }
}
