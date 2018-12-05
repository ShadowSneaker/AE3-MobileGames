using UnityEngine;
using System;

public class BezierSpline : MonoBehaviour
{
    public Vector3[] Points;

    public void Reset()
    {
        Points = new Vector3[]
        {
            new Vector3(1.0f, 0.0f, 0.0f),
            new Vector3(2.0f, 0.0f, 0.0f),
            new Vector3(3.0f, 0.0f, 0.0f),
            new Vector3(4.0f, 0.0f, 0.0f)
        };
    }


    public Vector3 GetPoint(float Value)
    {
        int i;
        if (Value >= 1.0f)
        {
            Value = 1.0f;
            i = Points.Length - 4;
        }
        else
        {
            Value = Mathf.Clamp01(Value) * CurveCount;
            i = (int)Value;
            Value -= i;
            i *= 3;
        }
        return transform.TransformPoint(Bezier.GetPoint(Points[i], Points[i + 1], Points[i + 2], Points[i + 3], Value));
    }


    public Vector3 GetVelocity(float Value)
    {
        int i;
        if (Value >= 1.0f)
        {
            Value = 1.0f;
            i = Points.Length - 4;
        }
        else
        {
            Value = Mathf.Clamp01(Value) * CurveCount;
            i = (int)Value;
            Value -= i;
            i *= 3;
        }
        return transform.TransformPoint(Bezier.GetFirstDerivative(Points[i], Points[i + 1], Points[i + 2], Points[i + 3], Value)) - transform.position;
    }


    public Vector3 GetDirection(float Value)
    {
        return GetVelocity(Value).normalized;
    }


    public void AddCurve()
    {
        Vector3 Point = Points[Points.Length - 1];
        Array.Resize(ref Points, Points.Length + 3);
        Point.x += 1.0f;
        Points[Points.Length - 3] = Point;
        Point.x += 1.0f;
        Points[Points.Length - 2] = Point;
        Point.x += 1.0f;
        Points[Points.Length - 1] = Point;
    }


    public int CurveCount
    {
        get
        {
            return ((Points.Length - 1) / 3);
        }
    }


}