using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.ComponentModel;

public class InputDisplay : MonoBehaviour
{
    [Header("Reference to Input Action Asset")]
    public InputActionAsset inputActions; 

    [Header("UI Text Fields")]

    public TMP_Text P1Text;
    public TMP_Text P2Text;

    public TMP_Text[] P1Movement = new TMP_Text[4];

    public TMP_Text[] P2Movement = new TMP_Text[4];

    string[] movement = { "Up: ", "Down: ", "Left: ", "Right: " };



    void Start()
    {

        GetSingleBindingDisplay("ButtonP1", P1Text, "Parry/Shoot: ");
        GetSingleBindingDisplay("ButtonP2", P2Text, "Parry/Shoot: ");

        GetBindingDisplay("AxisP1", P1Movement, movement);
        GetBindingDisplay("AxisP2", P2Movement, movement);
       

    }
    void GetSingleBindingDisplay(string actionName, TMP_Text textElement, string keybind)
{
    var action = inputActions.FindAction(actionName);
    if (action == null)
    {
        Debug.LogWarning($"Action '{actionName}' not found.");
        return;
    }

   
    if (action.bindings.Count > 0)
    {
        string displayName = InputControlPath.ToHumanReadableString(
            action.bindings[0].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice
        );
        textElement.text =  keybind + displayName;
    }
}

 void GetBindingDisplay(string actionPath, TMP_Text[] movement, string[] keybind)
{
    var action = inputActions.FindAction(actionPath);
    if (action == null)
    {
        Debug.LogWarning("Action not found: " + actionPath);
        return;
    }

    for (int i = 0; i < action.bindings.Count; i++)
    {
        var binding = action.bindings[i];

        if (binding.isComposite )
        {

                
                for (int j = 1; j <= 4; j++)
                {

                    var part = action.bindings[i + j];
                    string displayName = InputControlPath.ToHumanReadableString(
                        part.effectivePath,
                        InputControlPath.HumanReadableStringOptions.OmitDevice
                    );

                    movement[j-1].text = displayName;
                    
                }
            break;
        }
    }
}
}
