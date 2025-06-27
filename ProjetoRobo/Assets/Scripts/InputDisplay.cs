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

    private InputAction currentAction;

    private TMP_Text displayText;



    void Start()
    {

        //   .PerformInteractiveRebinding(bindingIndex)
        GetSingleBindingDisplay("ButtonP1", P1Text);
        GetSingleBindingDisplay("ButtonP2", P2Text);

        GetBindingDisplay("AxisP1", P1Movement, movement);
        GetBindingDisplay("AxisP2", P2Movement, movement);


    }
    void GetSingleBindingDisplay(string actionName, TMP_Text textElement)
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
            textElement.text = displayName;
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

            if (binding.isComposite)
            {


                for (int j = 1; j <= 4; j++)
                {

                    var part = action.bindings[i + j];
                    string displayName = InputControlPath.ToHumanReadableString(
                        part.effectivePath,
                        InputControlPath.HumanReadableStringOptions.OmitDevice
                    );

                    movement[j - 1].text = displayName;

                }
                break;
            }
        }
    }
    public void StartRebinding(string actionMapName, string actionName, TMP_Text text)
    {
        var actionMap = inputActions.FindActionMap(actionMapName);
        if (actionMap == null)
        {

            return;
        }

        currentAction = actionMap.FindAction(actionName);
        if (currentAction == null)
        {
            Debug.Log("NOPE!");
            return;
        }



        currentAction.Disable();

        currentAction
            .PerformInteractiveRebinding(0)
            .WithCancelingThrough("<Keyboard>/escape")
            .OnComplete(operation =>
            {
                currentAction.Enable();
                GetSingleBindingDisplay(actionName, text);
                operation.Dispose();

            })
            .Start();
    }
    public void RebindButtonP1()
    {
        StartRebinding("Normal", "ButtonP1", P1Text);
    }
    public void RebindButtonP2()
    {
        StartRebinding("Normal", "ButtonP2", P2Text);
    }
    int GetDirectionIndex(string direction)
    {
        switch (direction.ToLower())
        {
            case "up": return 0;
            case "down": return 1;
            case "left": return 2;
            case "right": return 3;
            default: return -1;
        }
    }
    public void StartRebindingMovement(string actionName, string compositePart, TMP_Text[] displayTextArray)
    {

        var action = inputActions.FindAction(actionName);
        if (action == null)
        {

            return;
        }

        int bindingIndex = -1;

        for (int i = 0; i < action.bindings.Count; i++)
        {
            var binding = action.bindings[i];

            if (binding.isPartOfComposite && binding.name == compositePart.ToLower())
            {

                bindingIndex = i;
                break;
            }
        }

        if (bindingIndex == -1)
        {
            Debug.LogWarning($"Composite part '{compositePart}' not found.");
            return;
        }
        displayText = displayTextArray[GetDirectionIndex(compositePart.ToLower())];
        action.actionMap.Disable();
        action.PerformInteractiveRebinding(bindingIndex)
            .WithCancelingThrough("<Keyboard>/escape")
            .OnComplete(operation =>
            {
                string newBinding = InputControlPath.ToHumanReadableString(
                action.bindings[bindingIndex].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice
                );

                if (displayText != null)
                    displayText.text = $"{newBinding}";

                operation.Dispose();
                action.actionMap.Enable();
            })
        .Start();
    }
    public void RebindMovementP1(string direction)
    {
        StartRebindingMovement("AxisP1", direction, P1Movement);
    }
    public void RebindMovementP2(string direction)
    {
        StartRebindingMovement("AxisP2",direction,P2Movement);
    }
}
