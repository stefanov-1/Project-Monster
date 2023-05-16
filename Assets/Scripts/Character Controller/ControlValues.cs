using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlValues : MonoBehaviour
{
    public static ControlValues Instance { get; private set; }

    public Vector3 currentClimbStart;
    public Vector3 currentClimbEnd;
    
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
