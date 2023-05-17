using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlValues : MonoBehaviour
{
    public static ControlValues Instance { get; private set; }

    public enum ClimbOrientation
    {
        UpDown,
        LeftRight
    }
    
    public Vector3 currentClimbStart;
    public Vector3 currentClimbEnd;
    public ClimbOrientation currentClimbOrientation = ClimbOrientation.LeftRight;
    
    private void Awake()
    {
        if (Instance is null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
