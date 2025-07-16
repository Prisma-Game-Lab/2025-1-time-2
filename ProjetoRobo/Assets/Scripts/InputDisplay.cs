using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.ComponentModel;
using UnityEngine.UI;
//using UnityEngine.InputSystem.Android;

public class InputDisplay : MonoBehaviour
{
    [Header("Reference to Input Action Asset")]
    public InputActionAsset inputActions;

    [Header("UI Text Fields")]

    [SerializeField] private GameObject RebindText;

    [SerializeField] private TMP_Text P1Text;
    [SerializeField] private TMP_Text P2Text;

    [SerializeField] private TMP_Text P1Switch;
    [SerializeField] private TMP_Text P2Switch;

    [SerializeField] private TMP_Text[] P1Movement = new TMP_Text[4];

    [SerializeField] private TMP_Text[] P2Movement = new TMP_Text[4];

    [Header("P1Buttons")]
    [SerializeField] private Button P1Button;

    [SerializeField] private Button P1Morph;
    [SerializeField] private Button[] P1MovementButtons = new Button[4];

    [Header("P2Buttons")]

    [SerializeField] private Button P2Button;

    [SerializeField] private Button P2Morph;

    [SerializeField] private Button[] P2MovementButtons = new Button[4];

    [Header("Buttons Sprites")]

    [SerializeField] private Sprite smallKey;

    [SerializeField] private Sprite longKey;

    string[] movement = { "Up: ", "Down: ", "Left: ", "Right: " };

    private InputAction currentAction;

    private TMP_Text displayText;



    void Start()
    {
       
        //   .PerformInteractiveRebinding(bindingIndex)
        GetSingleBindingDisplay("ButtonP1", P1Text,P1Button);
        GetSingleBindingDisplay("ButtonP2", P2Text,P2Button);

        GetSingleBindingDisplay("ChangeActionP1", P1Switch,P1Morph);
        GetSingleBindingDisplay("ChangeActionP2", P2Switch,P2Morph);

        GetBindingDisplay("AxisP1", P1Movement, movement,P1MovementButtons);
        GetBindingDisplay("AxisP2", P2Movement, movement,P2MovementButtons);


    }
    void GetSingleBindingDisplay(string actionName, TMP_Text textElement, Button button)
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
            if (displayName.Length == 1)
            {
                button.image.sprite = smallKey;
                button.image.SetNativeSize();
            }
            else
            {

                button.image.sprite = longKey;
                button.image.SetNativeSize();
            }
        }
    }

    void GetBindingDisplay(string actionPath, TMP_Text[] movement, string[] keybind, Button[] buttons)
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
                    if (displayName.Length == 1)
                    {
                    buttons[j-1].image.sprite = smallKey;
                    buttons[j-1].image.SetNativeSize();
                    }
                    else
                    {

                    buttons[j-1].image.sprite = longKey;
                    buttons[j-1].image.SetNativeSize();
                    }

                }
                break;
            }
        }
    }
    public void StartRebinding(string actionMapName, string actionName, TMP_Text text, Button button)
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
        RebindText.SetActive(true);
        text.text = "?";
        
        currentAction
            .PerformInteractiveRebinding(0)
            .WithCancelingThrough("<Keyboard>/escape")
            .OnComplete(operation =>
            {
                currentAction.Enable();
                RebindText.SetActive(false);
                GetSingleBindingDisplay(actionName, text, button);
                operation.Dispose();

            })
            .Start();
        
    }
    public void RebindButtonP1()
    {
        StartRebinding("Normal", "ButtonP1", P1Text,P1Button);
    }
    public void RebindButtonP2()
    {
        StartRebinding("Normal", "ButtonP2", P2Text,P2Button);
    }

    public void RebindSwitchP1()
    {
        StartRebinding("Normal", "ChangeActionP1", P1Switch,P1Morph);
    }
     public void RebindSwitchP2()
    {
        StartRebinding("Normal", "ChangeActionP2", P2Switch,P2Morph);
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
    public void StartRebindingMovement(string actionName, string compositePart, TMP_Text[] displayTextArray, Button[] buttons)
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
        displayText.text = "?";
        RebindText.SetActive(true);
        action.PerformInteractiveRebinding(bindingIndex)
            .WithCancelingThrough("<Keyboard>/escape")
            .OnComplete(operation =>
            {
                RebindText.SetActive(false);
                string newBinding = InputControlPath.ToHumanReadableString(
                action.bindings[bindingIndex].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice
                );

                if (displayText != null)
                    displayText.text = $"{newBinding}";
                if (newBinding.Length == 1)
                {
                    buttons[GetDirectionIndex(compositePart.ToLower())].image.sprite = smallKey;
                    buttons[GetDirectionIndex(compositePart.ToLower())].image.SetNativeSize();
                }
                else
                {

                    buttons[GetDirectionIndex(compositePart.ToLower())].image.sprite = longKey;
                    buttons[GetDirectionIndex(compositePart.ToLower())].image.SetNativeSize();
                    }

                operation.Dispose();
                action.actionMap.Enable();
            })
        .Start();
        
    }
    public void RebindMovementP1(string direction)
    {
        StartRebindingMovement("AxisP1", direction, P1Movement,P1MovementButtons);
    }
    public void RebindMovementP2(string direction)
    {
        StartRebindingMovement("AxisP2",direction,P2Movement,P2MovementButtons);
    }
}
