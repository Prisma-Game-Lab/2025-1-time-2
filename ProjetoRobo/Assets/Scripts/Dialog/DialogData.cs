using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogData", menuName = "ScriptableObjects/DialogData")]
public class DialogData : ScriptableObject
{
    //[TextArea]
    //public string[] DialogText;

    public SpeechData[] dialogueLines;
}

[Serializable]
public class SpeechData
{
    [TextArea]
    public string DialogText;
    [SerializeField]
    public Sprite SpeakerSprite; 
}
