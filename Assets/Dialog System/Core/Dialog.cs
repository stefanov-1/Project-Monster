using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog System/Dialog")]
public class Dialog : ScriptableObject
{
    public string identifier;
    public Sentence[] sentences;
}

[System.Serializable]
public class Sentence 
{
    [Header("Speaker")]
    public string name = "";

    // [TextArea(3, 10)]
    // public string sentence;
    public LocalizedString localizedSentence;
    public float delay = 0.03f;
}