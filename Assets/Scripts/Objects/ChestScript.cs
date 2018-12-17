using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : InteractableObject
{

    public bool Open;

    // The minimum amount of items that can be dropped.
    public int MinDropCount;

    // The maximum amount of items that can be dropped.
    public int MaxDropCount;

    // A list of items that may spawn.
    public GameObject[] DropObjects;


    // A list of items that are garenteed to spawn.
    public GameObject[] StaticItems;

    private Animator Anim;





	// Use this for initialization
	void Start ()
    {
        Anim = GetComponent<Animator>();

        if (Open)
        {
            Anim.enabled = true;


            StartCoroutine(DropItems());
        }
	}


    public override void Interact()
    {
        base.Interact();
        if (!Open)
        {
            Open = true;
            Anim.enabled = true;

            DropItems();
        }
    }


    private IEnumerator DropItems()
    {
        // Drop garenteed Items
        for (int i = 0; i < StaticItems.Length; ++i)
        {
            int Direction = Random.Range(-500, 500);
            int Height = Random.Range(800, 1000);
            GameObject SpawnedObject = Instantiate<GameObject>(StaticItems[i], transform.position, transform.rotation);
            SpawnedObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Direction, Height));
            yield return new WaitForSeconds(0.1f);
            SpawnedObject.transform.position = new Vector3(SpawnedObject.transform.position.x, SpawnedObject.transform.position.y, 0.0f);
        }

        // Drop Standard Items
        for (int i = 0; i < Random.Range(MinDropCount, MaxDropCount); ++i)
        {
            
            int Direction = Random.Range(-500, 500);
            int Height = Random.Range(800, 1000);
            GameObject SpawnedObject = Instantiate<GameObject>(DropObjects[Random.Range(0, DropObjects.Length)], transform.position, transform.rotation);
            SpawnedObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Direction, Height));
            yield return new WaitForSeconds(0.1f);
            SpawnedObject.transform.position = new Vector3(SpawnedObject.transform.position.x, SpawnedObject.transform.position.y, 0.0f);
        }

        
    }
}
