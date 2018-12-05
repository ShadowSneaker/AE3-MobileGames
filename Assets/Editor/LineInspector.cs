using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Line))]
public class LineInspector : Editor
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnSceneGUI()
    {
        Line line = target as Line;

        Transform HandleTransform = line.transform;
        Vector3 StartPos = HandleTransform.TransformPoint(line.StartPos);
        Vector3 EndPos = HandleTransform.TransformPoint(line.EndPos);

        Quaternion HandleRotation = (Tools.pivotRotation == PivotRotation.Local) ? HandleTransform.rotation : Quaternion.identity;

        Handles.color = Color.white;
        Handles.DrawLine(StartPos, EndPos);


        EditorGUI.BeginChangeCheck();
        StartPos = Handles.DoPositionHandle(StartPos, HandleRotation);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(line, "Move Point");
            EditorUtility.SetDirty(line);
            line.StartPos = HandleTransform.InverseTransformPoint(StartPos);
        }
        
        EditorGUI.BeginChangeCheck();
        EndPos = Handles.DoPositionHandle(EndPos, HandleRotation);
        
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(line, "Move Point");
            EditorUtility.SetDirty(line);
            line.EndPos = HandleTransform.InverseTransformPoint(EndPos);
        }
    }
}
