using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogActivator : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEngine.Events.UnityEvent onDialogFinished;

     [Header("Dialog")]
    [SerializeField] private DialogData dialog;

    private DialogData newDialog;
    private DialogController dialogController;

    private GameObject player;

    private InputActionAsset playerInput;

    private string teste;
    
   
    private void Awake()
    {

        dialogController = GameObject.FindWithTag("DialogController").GetComponent<DialogController>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (dialogController == null)
        {
            print("Dialog Controller not found in Scene");
        }

        newDialog = CloneDialog(dialog);
        playerInput = dialogController.inputActions;
        SubstituteKeybind();
        dialogController.OnDialogEnded += HandleDialogEnd;
    }

    public void ActivateDialog()
    {
        dialogController.StartDialog(newDialog);
    }
    private DialogData CloneDialog(DialogData original)
{
    DialogData clone = ScriptableObject.CreateInstance<DialogData>();
    clone.dialogueLines = new SpeechData[original.dialogueLines.Length];

    for (int i = 0; i < original.dialogueLines.Length; i++)
    {
        clone.dialogueLines[i] = new SpeechData
        {
            DialogText = original.dialogueLines[i].DialogText,
            SpeakerSprite = original.dialogueLines[i].SpeakerSprite
        };
    }

    return clone;
}
    private void SubstituteKeybind()
    {
        bool P1Movement = player.GetComponent<InputManager>().P1Movement;
        // PROBLEM
        // NEED TO KEEP TRACK TO WHICH MORPH BUTTON IS ACTIVE:
        // SOLUTION USE <MORPHDEFENSVE> AND <MORPHOPHENSIVE> INSTEAD OF <MORPHP1> AND <MORPHP2>
        //newDialog = CloneDialog(dialog);
        for (int i = 0; i < newDialog.dialogueLines.Length; i++)
        {
            string line = newDialog.dialogueLines[i].DialogText;

            if (line.Contains("<MOVEP1>"))
            {
                line = line.Replace("<MOVEP1>", GetMovementKeybinds("AxisP1"));
            }
            if (line.Contains("<MOVEP2>"))
            {
                line = line.Replace("<MOVEP2>", GetMovementKeybinds("AxisP2"));
            }
            if (line.Contains("<BUTTONP1>"))
            {
                line = line.Replace("<BUTTONP1>", GetSingleKeybind("ButtonP1"));
            }
            if (line.Contains("<BUTTONP2>"))
            {
                line = line.Replace("<BUTTONP2>", GetSingleKeybind("ButtonP2"));
            }
            if (line.Contains("<MORPHOFFENSIVE>"))
            {
                // IF P1MOVEMENT -> P1 HAS OFFENSIVE
                if (P1Movement)
                    line = line.Replace("<MORPHOFFENSIVE>", GetSingleKeybind("MorphP1"));
                else
                    line = line.Replace("<MORPHOFFENSIVE>", GetSingleKeybind("MorphP2"));
            }
            if (line.Contains("<MORPHDEFENSIVE>"))
            {
                if (P1Movement)
                    line = line.Replace("<MORPHDEFENSIVE>", GetSingleKeybind("MorphP2"));
                else
                    line = line.Replace("<MORPHDEFENSIVE>", GetSingleKeybind("MorphP1"));
            }
            if (line.Contains("<TEXTADV>"))
            {
                line = line.Replace("<TEXTADV>", GetSingleKeybind("AdvanceText"));
            }

            newDialog.dialogueLines[i].DialogText = line;
        }
    }

    private string GetSingleKeybind(string actionName)
    {
        var action = playerInput.FindAction(actionName);
        if (action == null)
        {
            Debug.LogWarning($"Action '{actionName}' not found.");
            return "";
        }


        if (action.bindings.Count > 0)
        {
            string displayName = InputControlPath.ToHumanReadableString(
                action.bindings[0].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice
            );
            return displayName;
        }
        return "";
    }
    private string GetMovementKeybinds(string actionPath)
    {
        string mov = "";
        var action = playerInput.FindAction(actionPath);

        if (action == null)
        {
            Debug.LogWarning("Action not found: " + actionPath);
            return "";
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

                    mov += displayName;
                    if (j < 4)
                    {
                        mov += "/";
                    }

                }
                break;
            }

        }
        return mov;
    }
    private void HandleDialogEnd(DialogData endedDialog)
    {
        if (endedDialog == newDialog)
        {

            onDialogFinished?.Invoke();

        }
    }

    private void OnDestroy()
    {
        
        if (dialogController != null)
            dialogController.OnDialogEnded -= HandleDialogEnd;
    }
}
