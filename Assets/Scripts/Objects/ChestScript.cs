using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : InteractableObject
{

    public ItemPickup SpawnPrefab;
    

    // The minimum amount of items that can be dropped.
    public int MinDropCount;

    // The maximum amount of items that can be dropped.
    public int MaxDropCount;

    // A list of items that may spawn.
    public ItemScript[] DropObjects;


    // A list of items that are garenteed to spawn.
    public ItemScript[] StaticItems;

    private Animator Anim;





	// Use this for initialization
	void Start ()
    {
        Anim = GetComponent<Animator>();

        if (Active)
        {
            Anim.enabled = true;


            StartCoroutine(DropItems());
        }
	}


    public override void Interact()
    {
        base.Interact();
        if (!Active)
        {
            Active = true;
            Anim.enabled = true;

            StartCoroutine(DropItems());
        }
    }


    public void OnTriggerEnter2D(Collider2D Collision)
    {
        Player OBJ = Collision.GetComponent<Player>();
        if (OBJ && !OBJ.Interact)
        {
            OBJ.Interact = this;
        }
    }


    public void OnTriggerExit2D(Collider2D Collision)
    {
        Player OBJ = Collision.GetComponent<Player>();
        if (OBJ && OBJ.Interact == this)
        {
            OBJ.Interact = null;
        }
    }


    private IEnumerator DropItems()
    {
        // Drop garenteed Items
        for (int i = 0; i < StaticItems.Length; ++i)
        {
            int Direction = Random.Range(-500, 500);
            int Height = Random.Range(800, 1000);

            ItemPickup SpawnedObject = Instantiate<ItemPickup>(SpawnPrefab, transform.position, transform.rotation);
            SpawnedObject.Item = StaticItems[i];
            SpawnedObject.GetComponent<SpriteRenderer>().sprite = SpawnedObject.Item.Image;

            SpawnedObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Direction, Height));
            yield return new WaitForSeconds(0.1f);
            SpawnedObject.transform.position = new Vector3(SpawnedObject.transform.position.x, SpawnedObject.transform.position.y, 0.0f);
        }

        // Drop Standard Items
        for (int i = 0; i < Random.Range(MinDropCount, MaxDropCount); ++i)
        {
            int Direction = Random.Range(-500, 500);
            int Height = Random.Range(800, 1000);

            ItemPickup SpawnedObject = Instantiate<ItemPickup>(SpawnPrefab, transform.position, transform.rotation);
            SpawnedObject.Item = DropObjects[Random.Range(0, DropObjects.Length)];
            SpawnedObject.GetComponent<SpriteRenderer>().sprite = SpawnedObject.Item.Image;

            SpawnedObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Direction, Height));
            yield return new WaitForSeconds(0.1f);
            SpawnedObject.transform.position = new Vector3(SpawnedObject.transform.position.x, SpawnedObject.transform.position.y, 0.0f);
        }   
    }
}
