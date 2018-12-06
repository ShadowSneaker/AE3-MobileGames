using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Bezier
{
    public static Vector3 GetPoint(Vector3 P0, Vector3 P1, Vector3 P2, Vector3 P3, float Value)
    {
        Value = Mathf.Clamp01(Value);
        float OneMinus = 1.0f - Value;
        return OneMinus * OneMinus * OneMinus * P0 + 3.0f * OneMinus * OneMinus * Value * P1 + 3.0f * OneMinus * (Value * Value) * P2 + (Value * Value * Value) * P3;
    }


    public static Vector3 GetFirstDerivative(Vector3 P0, Vector3 P1, Vector3 P2, Vector3 P3, float Value)
    {
        Value = Mathf.Clamp01(Value);
        float OneMinus = 1.0f - Value;
        return 3.0f * OneMinus * OneMinus * (P1 - P0) + 6.0f * OneMinus * Value * (P2 - P1) + 3.0f * Value * Value * (P3 - P2);
    }


    
}
