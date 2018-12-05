using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    public Vector3[] Points;

    public void Reset()
    {
        Points = new Vector3[]
        {
            new Vector3(1.0f, 0.0f, 0.0f),
            new Vector3(2.0f, 0.0f, 0.0f),
            new Vector3(3.0f, 0.0f, 0.0f)
        };
    }

    
    public Vector3 GetPoint(float Value)
    {
        return transform.TransformPoint(Bezier.GetPoint(Points[0], Points[1], Points[2], Value));
    }
}
