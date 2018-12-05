using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BezierSpline))]
public class BezierSplineInspector : Editor
{
    private BezierSpline Spline;
    private Transform HandleTransform;
    private Quaternion HandleRotation;
    private const float DirectionScale = 0.5f;
    private const int StepsPerCurve = 10;

    private const int LineSteps = 10;

    private const float HandleSize = 0.04f;
    private const float PickSize = 0.06f;

    private int SelectedIndex = -1;

    private void OnSceneGUI()
    {
        Spline = target as BezierSpline;
        HandleTransform = Spline.transform;
        HandleRotation = (Tools.pivotRotation == PivotRotation.Local) ? HandleTransform.rotation : Quaternion.identity;

        Vector3 P0 = ShowPoint(0);
        for (int i = 1; i < Spline.Points.Length; i += 3)
        {
            Vector3 P1 = ShowPoint(i);
            Vector3 P2 = ShowPoint(i + 1);
            Vector3 P3 = ShowPoint(i + 2);

            Handles.color = Color.gray;
            Handles.DrawLine(P0, P1);
            Handles.DrawLine(P2, P3);

            Handles.DrawBezier(P0, P3, P1, P2, Color.white, null, 2.0f);
            P0 = P3;
        }

        ShowDirection();
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        DrawDefaultInspector();
        Spline = target as BezierSpline;
        if (GUILayout.Button("Add Curve"))
        {
            Spline.AddCurve();
            EditorUtility.SetDirty(Spline);
        }
    }


    private void ShowDirection()
    {
        Handles.color = Color.green;
        Vector3 Point = Spline.GetPoint(0.0f);
        Handles.DrawLine(Point, Point + Spline.GetDirection(0.0f) * DirectionScale);
        int Steps = StepsPerCurve * Spline.CurveCount;
        for (int i = 1; i <= Steps; i++)
        {
            Point = Spline.GetPoint(i / (float)Steps);
            Handles.DrawLine(Point, Point + Spline.GetDirection(i / (float)Steps) * DirectionScale);
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
        Vector3 Point = HandleTransform.TransformPoint(Spline.Points[Index]);
        Handles.color = Color.white;
        if (Handles.Button(Point, HandleRotation, HandleSize, PickSize, Handles.DotCap)) // Use Handles.CapFunction
        {
            SelectedIndex = Index;
        }

        if (SelectedIndex == Index)
        {

            EditorGUI.BeginChangeCheck();
            Point = Handles.DoPositionHandle(Point, HandleRotation);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(Spline, "Move Point");
                EditorUtility.SetDirty(Spline);
                Spline.Points[Index] = HandleTransform.InverseTransformPoint(Point);
            }
        }
        return Point;
    }
}
