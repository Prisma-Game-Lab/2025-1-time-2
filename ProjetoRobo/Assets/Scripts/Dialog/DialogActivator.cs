using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    [SerializeField] private DialogData dialog;

    private DialogController dialogController;

    private void Awake()
    {
        dialogController = GameObject.FindWithTag("DialogController").GetComponent<DialogController>();
        
        if(dialogController == null) 
        {
            print("Dialog Controller not found in Scene");
        }
    }

    public void ActivateDialog() 
    {
        dialogController.StartDialog(dialog);
    }
}
