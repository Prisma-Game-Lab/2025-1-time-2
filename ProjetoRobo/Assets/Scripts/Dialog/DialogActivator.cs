using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEngine.Events.UnityEvent onDialogFinished;

     [Header("Dialog")]
    [SerializeField] private DialogData dialog;

    private DialogController dialogController;

    private void Awake()
    {
        dialogController = GameObject.FindWithTag("DialogController").GetComponent<DialogController>();

        if (dialogController == null)
        {
            print("Dialog Controller not found in Scene");
        }

        dialogController.OnDialogEnded += HandleDialogEnd;
    }

    public void ActivateDialog()
    {
        dialogController.StartDialog(dialog);
    }
    
    private void HandleDialogEnd(DialogData endedDialog)
    {
        if (endedDialog == dialog)
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
