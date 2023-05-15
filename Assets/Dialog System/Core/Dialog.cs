using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog System/Dialog")]
public class Dialog : ScriptableObject
{
    public Sentence[] sentences;
}

[System.Serializable]
public class Sentence 
{
    public string name = "";

    [TextArea(3, 10)]
    public string sentence;
}