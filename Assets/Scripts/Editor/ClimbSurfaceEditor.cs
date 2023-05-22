using System;
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
            Transform collision = climbSurface.Collision;
            Vector3 startPoint = climbSurface.startPoint.position;
            Vector3 endPoint = climbSurface.endPoint.position;

            collision.position = (startPoint + endPoint) / 2;
            collision.LookAt(endPoint);
            collision.localScale = new Vector3(1, 1, Vector3.Distance(startPoint, endPoint));
            
        }
    }
    
}
