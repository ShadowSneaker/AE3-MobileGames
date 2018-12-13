using System.Collections.Generic;
using UnityEngine;
using System;

public enum BezierControlPointMode
{
    Free,
    Aligned,
    Mirrored
}

public class BezierSpline : MonoBehaviour
{
    /// Properties
    
    // An array of all modifiable points in the Spline.
    [SerializeField]
    public Vector3[] Points;

    // A mode corosponding to each point allowing different effects to be applied to the spline (at the corosponding point).
    [SerializeField]
    private BezierControlPointMode[] Modes;

    // Specifies if the spline should loop connecting the first point with the last (and sharing the tangents).
    [SerializeField]
    private bool loop;
    


    // On Reset, Set Spline Default setup.
    public void Reset()
    {
        Points = new Vector3[]
        {
            new Vector3(1.0f, 0.0f, 0.0f),
            new Vector3(2.0f, 0.0f, 0.0f),
            new Vector3(3.0f, 0.0f, 0.0f),
            new Vector3(4.0f, 0.0f, 0.0f)
        };

        Modes = new BezierControlPointMode[]
        {
            BezierControlPointMode.Mirrored,
            BezierControlPointMode.Mirrored
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

        Array.Resize(ref Modes, Modes.Length + 1);
        Modes[Modes.Length - 1] = Modes[Modes.Length - 2];
        EnforceMode(Points.Length - 4);

        if (loop)
        {
            Points[Points.Length - 1] = Points[0];
            Modes[Modes.Length - 1] = Modes[0];
            EnforceMode(0);
        }
    }


    public BezierControlPointMode GetControlPointMode(int Index)
    {
        return Modes[(Index + 1) / 3];
    }


    public void SetControlPointMode(int Index, BezierControlPointMode Mode)
    {
        int ModeIndex = (Index + 1) / 3;
        Modes[ModeIndex] = Mode;
        if (loop)
        {
            if (ModeIndex == 0)
            {
                Modes[Modes.Length - 1] = Mode;
            }
            else if(ModeIndex == Modes.Length - 1)
            {
                Modes[0] = Mode;
            }
        }
        EnforceMode(Index);
    }


    public Vector3 GetControlPoint(int Index)
    {
        return Points[Index];
    }


    public void SetControlPoint (int Index, Vector3 Point)
    {
        if (Index % 3 == 0)
        {
            Vector3 Delta = Point - Points[Index];
            if (loop)
            {
                if (Index == 0)
                {
                    Points[1] += Delta;
                    Points[Points.Length - 2] += Delta;
                    Points[Points.Length - 1] = Point;
                }
                else if (Index == Points.Length - 1)
                {
                    Points[0] = Point;
                    Points[1] += Delta;
                    Points[Index - 1] += Delta;
                }
                else
                {
                    Points[Index - 1] += Delta;
                    Points[Index + 1] += Delta;
                }
            }
            else
            {
                if (Index > 0)
                {
                    Points[Index - 1] += Delta;
                }
                if (Index + 1 < Points.Length)
                {
                    Points[Index + 1] += Delta;
                }
            }
        }

        Points[Index] = Point;
        EnforceMode(Index);
    }


    private void EnforceMode(int Index)
    {
        int ModeIndex = (Index + 1) / 3;
        BezierControlPointMode Mode = Modes[ModeIndex];
        if (Mode == BezierControlPointMode.Free || !loop && (ModeIndex == 0 || ModeIndex == Modes.Length - 1))
        {
            return;
        }

        int MiddleIndex = ModeIndex * 3;
        int FixedIndex, EnforcedIndex;
        if (Index <= MiddleIndex)
        {
            FixedIndex = MiddleIndex - 1;
            if (FixedIndex < 0)
            {
                FixedIndex = Points.Length - 2;
            }

            EnforcedIndex = MiddleIndex + 1;
            if (EnforcedIndex >= Points.Length)
            {
                EnforcedIndex = 1;
            }
        }
        else
        {
            FixedIndex = MiddleIndex + 1;
            if (FixedIndex >= Points.Length)
            {
                FixedIndex = 1;
            }

            EnforcedIndex = MiddleIndex - 1;
            if (EnforcedIndex < 0)
            {
                EnforcedIndex = Points.Length - 2;
            }
        }

        Vector3 Middle = Points[MiddleIndex];
        Vector3 EnforcedTangent = Middle - Points[FixedIndex];
        if (Mode == BezierControlPointMode.Aligned)
        {
            EnforcedTangent = EnforcedTangent.normalized * Vector3.Distance(Middle, Points[EnforcedIndex]);
        }
        Points[EnforcedIndex] = Middle + EnforcedTangent;
    }



    public int CurveCount
    {
        get
        {
            return ((Points.Length - 1) / 3);
        }
    }

    public int ControlPointCount
    {
        get
        {
            return Points.Length;
        }
    }


    public bool Loop
    {
        get
        {
            return loop;
        }
        set
        {
            loop = value;
            if (value == true)
            {
                Modes[Modes.Length - 1] = Modes[0];
                SetControlPoint(0, Points[0]);
            }
        }
    }
}