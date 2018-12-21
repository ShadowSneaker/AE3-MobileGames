using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemScript Item;
    private bool CanBePickedUp = false;


    private void Start()
    {
        if (Item && Item.Image)
        {
            GetComponent<SpriteRenderer>().sprite = Item.Image;
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (CanBePickedUp && collision.CompareTag("Player"))
        {
            // Put Item into Inventory.
            Entity Other = collision.GetComponent<Player>();
            Item.Use(Other);
            Destroy(gameObject);
        }
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            CanBePickedUp = true;
        }
    }
}
