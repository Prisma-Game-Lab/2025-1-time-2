using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogData", menuName = "ScriptableObjects/DialogData")]
public class DialogData : ScriptableObject
{
    [TextArea]
    public string[] DialogText;
}
