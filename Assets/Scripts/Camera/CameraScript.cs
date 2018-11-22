using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject FollowObject;

    public float MaxSpeed = 2.0f;

    public bool LockOnObject = false;

    public bool SnapOnStart = true;



	// Use this for initialization
	void Start ()
    {
		if (SnapOnStart)
        {
            transform.position = new Vector3(FollowObject.transform.position.x, FollowObject.transform.position.y, transform.position.z);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (FollowObject)
        {
            if (LockOnObject)
            {
                transform.position = new Vector3(FollowObject.transform.position.x, FollowObject.transform.position.y, transform.position.z);

            }
            else
            {
                Vector3 Pos = transform.position;
                Pos.y = Mathf.Lerp(transform.position.y, FollowObject.transform.position.y, MaxSpeed * Time.deltaTime);
                Pos.x = Mathf.Lerp(transform.position.x, FollowObject.transform.position.x, MaxSpeed * Time.deltaTime);

                transform.position = Pos;
            }
        }
    }
}
