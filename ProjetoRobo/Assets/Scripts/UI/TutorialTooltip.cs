using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine;

public class TutorialTooltip : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private InputActionAsset playerInput;

    [SerializeField] private float duration;

    private void Start()
    {
        StartCoroutine(Dissapear());
        SubstituteKeybind();
    }
    public IEnumerator Dissapear()
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
    private void SubstituteKeybind()
    {
        //newDialog = CloneDialog(dialog);

        string line = text.text;

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
        if (line.Contains("<MORPHP1>"))
        {
            line = line.Replace("<MORPHP1>", GetSingleKeybind("ChangeActionP1"));
        }
        if (line.Contains("<MORPHP2>"))
        {
            line = line.Replace("<MORPHP2>", GetSingleKeybind("ChangeActionP2"));
        }
        if (line.Contains("<TEXTADV>"))
        {
            line = line.Replace("<TEXTADV>", GetSingleKeybind("AdvanceText"));
        }

        text.text = line;
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
}
