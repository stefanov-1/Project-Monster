using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof(DeathSurface))]
public class DeathSurfaceEditor : Editor
{
    private DeathSurface deathSurface;

    private void OnEnable()
    {
        deathSurface = (DeathSurface)target;
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
        if (deathSurface is null)
            return;
        
        StartEndHandles();
    }
    
    void StartEndHandles()
    {
        Transform handleTransform = deathSurface.transform;

        EditorGUI.BeginChangeCheck();
        Vector3 newStartPos = Handles.PositionHandle(deathSurface.startPoint.position, Quaternion.identity);
        Vector3 newEndPos = Handles.PositionHandle(deathSurface.endPoint.position, Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(deathSurface, "changed start or end position");
            EditorUtility.SetDirty(deathSurface);
            deathSurface.startPoint.position = newStartPos;
            deathSurface.endPoint.position = newEndPos;
            Apply();
        }
    }

    void Apply()
    {
        Transform collision = deathSurface.Collision;
        Vector3 startPoint = deathSurface.startPoint.position;
        Vector3 endPoint = deathSurface.endPoint.position;

        collision.position = (startPoint + endPoint) / 2;
        collision.LookAt(endPoint);
        collision.localScale = new Vector3(1, 1, Vector3.Distance(startPoint, endPoint));
    }
    
}
