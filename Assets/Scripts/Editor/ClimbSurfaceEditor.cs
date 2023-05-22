using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof(ClimbSurface))]
public class ClimbSurfaceEditor : Editor
{
    private ClimbSurface climbSurface;

    private void OnEnable()
    {
        climbSurface = (ClimbSurface)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Apply"))
        {
            Apply();
        }
    }

    public void OnSceneGUI()
    {
        if (climbSurface is null)
            return;
        
        StartEndHandles();
    }
    
    void StartEndHandles()
    {
        Transform handleTransform = climbSurface.transform;

        EditorGUI.BeginChangeCheck();
        Vector3 newStartPos = Handles.PositionHandle(climbSurface.startPoint.position, Quaternion.identity);
        Vector3 newEndPos = Handles.PositionHandle(climbSurface.endPoint.position, Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(climbSurface, "changed start or end position");
            EditorUtility.SetDirty(climbSurface);
            climbSurface.startPoint.position = newStartPos;
            climbSurface.endPoint.position = newEndPos;
            Apply();
        }
    }

    void Apply()
    {
        Transform collision = climbSurface.Collision;
        Vector3 startPoint = climbSurface.startPoint.position;
        Vector3 endPoint = climbSurface.endPoint.position;

        collision.position = (startPoint + endPoint) / 2;
        collision.LookAt(endPoint);
        collision.localScale = new Vector3(1, 1, Vector3.Distance(startPoint, endPoint));
    }
    
}
