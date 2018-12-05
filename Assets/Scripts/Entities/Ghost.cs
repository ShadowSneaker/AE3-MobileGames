using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private Entity Player;
    private Entity Self;

	// Use this for initialization
	void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>();
        Self = GetComponent<Entity>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Player)
        {
            //Vector3 NewPos;

            //NewPos = Vector2.MoveTowards(transform.position, Player.transform.position, Self.MovementSpeed * Time.deltaTime);

            Self.MoveSideways((transform.position.x > Player.transform.position.x) ? -1.0f : 1.0f);

            
        }
	}
}
