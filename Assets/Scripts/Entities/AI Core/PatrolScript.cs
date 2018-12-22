using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolScript : MonoBehaviour
{
    // The order of positions this object moves to.
    public AINode[] PatrolPoints;

    // what Patrol Point this object starts at.
    public int CurrentPoint = 0;

    // The direction the object will be moving in.
    public bool Reverse = false;


    private Entity PatrolObject;

    internal float Direction;


	// Use this for initialization
	void Start ()
    {
        PatrolObject = GetComponent<Entity>();

        for (int i = 0; i < PatrolPoints.Length; ++i)
        {
            PatrolPoints[i].Owner = this;
            PatrolPoints[i].ENT = PatrolObject;
            PatrolPoints[i].NodeNum = i;
        }

        Direction = (PatrolObject.transform.position.x > PatrolPoints[CurrentPoint].transform.position.x) ? -1 : 1;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (PatrolObject && !PatrolObject.IsDead())
        {
            PatrolObject.MoveSideways(Direction);
        }
	}


    public void GoToNextPoint()
    {
        if (CurrentPoint >= PatrolPoints.Length - 1)
        {
            Reverse = true;
        }
        else if (CurrentPoint <= 0)
        {
            Reverse = false;
        }

        CurrentPoint += (Reverse) ? -1 : 1;
        

        Direction = (PatrolObject.transform.position.x > PatrolPoints[CurrentPoint].transform.position.x) ? -1 : 1;
    }
}
