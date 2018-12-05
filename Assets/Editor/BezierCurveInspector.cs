using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BezierCurve))]
public class BezierCurveInspector : Editor
{
    private BezierCurve Curve;
    private Transform HandleTransform;
    private Quaternion HandleRotation;

    private const int LineSteps = 10;

    private void OnSceneGUI()
    {
        Curve = target as BezierCurve;
        HandleTransform = Curve.transform;
        HandleRotation = (Tools.pivotRotation == PivotRotation.Local) ? HandleTransform.rotation : Quaternion.identity;

        Vector3 P0 = ShowPoint(0);
        Vector3 P1 = ShowPoint(1);
        Vector3 P2 = ShowPoint(2);

        Handles.color = Color.gray;
        Handles.DrawLine(P0, P1);
        Handles.DrawLine(P1, P2);

        Handles.color = Color.white;
        Vector3 LineStart = Curve.GetPoint(0.0f);
        for (int i = 0; i < LineSteps; ++i)
        {
            Vector3 LineEnd = Curve.GetPoint(i / (float)LineSteps);
            Handles.DrawLine(LineStart, LineEnd);
            LineStart = LineEnd;
        }
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
