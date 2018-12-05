using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BezierCurve))]
public class BezierCurveInspector : Editor
{
    private BezierCurve Curve;
    private Transform HandleTransform;
    private Quaternion HandleRotation;
    private const float DirectionScale = 0.5f;

    private const int LineSteps = 10;

    private void OnSceneGUI()
    {
        Curve = target as BezierCurve;
        HandleTransform = Curve.transform;
        HandleRotation = (Tools.pivotRotation == PivotRotation.Local) ? HandleTransform.rotation : Quaternion.identity;

        Vector3 P0 = ShowPoint(0);
        Vector3 P1 = ShowPoint(1);
        Vector3 P2 = ShowPoint(2);
        Vector3 P3 = ShowPoint(3);

        Handles.color = Color.gray;
        Handles.DrawLine(P0, P1);
        Handles.DrawLine(P2, P3);

        ShowDirection();
        Handles.DrawBezier(P0, P3, P1, P2, Color.white, null, 2.0f);
    }


    private void ShowDirection()
    {
        Handles.color = Color.green;
        Vector3 Point = Curve.GetPoint(0.0f);
        Handles.DrawLine(Point, Point + Curve.GetDirection(0.0f) * DirectionScale);
        for (int i = 0; i < LineSteps; ++i)
        {
            Point = Curve.GetPoint(i / (float)LineSteps);
            Handles.DrawLine(Point, Point + Curve.GetDirection(i / (float)LineSteps) * DirectionScale);
        }

        //Handles.color = Color.white;
        //Vector3 LineStart = Curve.GetPoint(0.0f);
        //Handles.color = Color.green;
        //Handles.DrawLine(LineStart, LineStart + Curve.GetDirection(0.0f));
        //for (int i = 0; i < LineSteps; ++i)
        //{
        //    Vector3 LineEnd = Curve.GetPoint(i / (float)LineSteps);
        //    Handles.color = Color.white;
        //    Handles.DrawLine(LineStart, LineEnd);
        //    Handles.color = Color.green;
        //    Handles.DrawLine(LineEnd, LineEnd + Curve.GetDirection(i / (float)LineSteps));
        //    LineStart = LineEnd;
        //}
    }


    private Vector3 ShowPoint(int Index)
    {
        Vector3 Point = HandleTransform.TransformPoint(Curve.Points[Index]);
        EditorGUI.BeginChangeCheck();
        Point = Handles.DoPositionHandle(Point, HandleRotation);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(Curve, "Move Point");
            EditorUtility.SetDirty(Curve);
            Curve.Points[Index] = HandleTransform.InverseTransformPoint(Point);
        }
        
        return Point;
    }


    
}
