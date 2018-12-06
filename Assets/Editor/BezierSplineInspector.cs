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

    private static Color[] ModeColors =
    {
        Color.white,
        Color.yellow,
        Color.cyan
    };



    private void OnSceneGUI()
    {
        Spline = target as BezierSpline;
        HandleTransform = Spline.transform;
        HandleRotation = (Tools.pivotRotation == PivotRotation.Local) ? HandleTransform.rotation : Quaternion.identity;
        
        Vector3 P0 = ShowPoint(0);
        for (int i = 1; i < Spline.ControlPointCount; i += 3)
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
        Spline = target as BezierSpline;
        EditorGUI.BeginChangeCheck();
        bool Loop = EditorGUILayout.Toggle("Loop", Spline.Loop);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(Spline, "Toggle Loop");
            EditorUtility.SetDirty(Spline);
            Spline.Loop = Loop;
        }

        if (SelectedIndex >= 0 && SelectedIndex < Spline.ControlPointCount)
        {
            DrawSelectedPointInspector();
        }

        if (GUILayout.Button("Add Curve"))
        {
            Undo.RecordObject(Spline, "Add Curve");
            Spline.AddCurve();
            EditorUtility.SetDirty(Spline);
        }
    }


    private void DrawSelectedPointInspector()
    {
        GUILayout.Label("Selected Point");
            
        EditorGUI.BeginChangeCheck();
        Vector3 Point = EditorGUILayout.Vector3Field("Position", Spline.GetControlPoint(SelectedIndex));
        
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(Spline, "Move Point");
            EditorUtility.SetDirty(Spline);
            Spline.SetControlPoint(SelectedIndex, Point);
        }

        EditorGUI.BeginChangeCheck();
        BezierControlPointMode Mode = (BezierControlPointMode)EditorGUILayout.EnumPopup("Mode", Spline.GetControlPointMode(SelectedIndex));
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(Spline, "Change Point Mode");
            Spline.SetControlPointMode(SelectedIndex, Mode);
            EditorUtility.SetDirty(Spline);
        }
    }


    private void ShowDirection()
    {
        Handles.color = Color.green;
        Vector3 Point = Spline.GetPoint(0.0f);
        Vector3 LastPoint = Point;
        Handles.DrawLine(Point, Point + Spline.GetDirection(0.0f) * DirectionScale);
        int Steps = StepsPerCurve * Spline.CurveCount;
        for (int i = 1; i <= Steps; i++)
        {
            Point = Spline.GetPoint(i / (float)Steps);
            
            // Gets the distance between each segment - Use this to calculate the distance between points.
            float Mag = 0.0f;
            if (i + 1 <= Steps)
            {
                Mag = Vector3.Distance(Point, Spline.GetPoint((i + 1) / (float)Steps));
            }
            Handles.DrawLine(Point, Point + Spline.GetDirection(i / (float)Steps) * Mag);
            LastPoint = Point;
        }
    }


    private Vector3 ShowPoint(int Index)
    {
        Vector3 Point = HandleTransform.TransformPoint(Spline.GetControlPoint(Index));
        float Size = HandleUtility.GetHandleSize(Point);
        if (Index == 0)
        {
            Size *= 2f;
        }

        Handles.color = ModeColors[(int)Spline.GetControlPointMode(Index)];
        if (Handles.Button(Point, HandleRotation, Size * HandleSize, Size * PickSize, Handles.DotCap)) // Use Handles.CapFunction
        {
            SelectedIndex = Index;
            Repaint();
        }

        if (SelectedIndex == Index)
        {

            EditorGUI.BeginChangeCheck();
            Point = Handles.DoPositionHandle(Point, HandleRotation);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(Spline, "Move Point");
                EditorUtility.SetDirty(Spline);
                Spline.SetControlPoint(Index, HandleTransform.InverseTransformPoint(Point));
            }
        }
        return Point;
    }
}
