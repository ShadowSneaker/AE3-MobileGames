﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public enum ConveyorMode
{
    Once,
    Loop,
    PingPong
}


public class Conveyor : MonoBehaviour
{

    public ConveyorMode Mode;

    public float Duration;
    
    public bool Rotate;

    public bool IsChildObject = true;

    public Transform MoveObject;

    public float Speed;
    float Distance = 0;


    private BezierSpline Spline;

    private float Progress;

    private bool MovingForward = true;




    private void Start()
    {
        Spline = GetComponent<BezierSpline>();
        Spline.SetLength();
    }


    private void Update()
    {
        if (MoveObject)
        {
            if (MovingForward)
            {
                Progress += Time.deltaTime / Duration;
                Distance += Speed * Time.deltaTime;
                if (Progress > 1.0f)
                {
                    switch (Mode)
                    {
                        case ConveyorMode.Once:
                            Progress = 1.0f;
                            break;


                        case ConveyorMode.Loop:
                            Progress -= 1.0f;
                            break;


                        case ConveyorMode.PingPong:
                            Progress = 2.0f - Progress;
                            MovingForward = false;
                            break;
                    }
                }
            }
            else
            {
                Progress -= Time.deltaTime / Duration;
                Distance -= Speed * Time.deltaTime;

                if (Progress < 0.0f)
                {
                    Progress = -Progress;
                    MovingForward = true;
                }
            }

            Debug.Log(Distance);



            Vector3 Position = Spline.GetPoint(Distance / Spline.TotalLength) - ((IsChildObject) ? transform.position : new Vector3(0.0f, 0.0f, 0.0f));
            MoveObject.transform.localPosition = Position;
            if (Rotate)
            {
                MoveObject.transform.LookAt(Position + Spline.GetDirection(Progress));
            }
        }
    }
}
