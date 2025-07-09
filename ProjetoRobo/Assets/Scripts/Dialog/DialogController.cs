using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject dialogHolder;
    [SerializeField] private TMP_Text textField;

    [Header("Variables")]
    [SerializeField] private float timeBetweenCharacters;

    private DialogData currentDialogData;
    private int currentDialogIndex;
    private bool onDialog = false;
    private bool writingSentence = false;
    private Coroutine writeSentenceCoroutine;

    public void StartDialog(DialogData dialogData) 
    {
        if (onDialog) return;

        onDialog = true;
        dialogHolder.SetActive(true);

        textField.text = "";
        currentDialogData = dialogData;
        currentDialogIndex = 0;
        Time.timeScale = 0;
        writeSentenceCoroutine = StartCoroutine(WriteSentence());
    }

    public void AdvanceDialog() 
    {
        if (writingSentence) 
        {
            StopCoroutine(writeSentenceCoroutine);
            textField.text = currentDialogData.DialogText[currentDialogIndex];
            writingSentence = false;
            return;
        }

        currentDialogIndex++;
        if (currentDialogIndex >= currentDialogData.DialogText.Length) 
        {
            EndDialog();
            return;
        }

        writeSentenceCoroutine = StartCoroutine(WriteSentence());
    }

    private IEnumerator WriteSentence() 
    {
        writingSentence = true;
        textField.text = "";
        foreach (char character in currentDialogData.DialogText[currentDialogIndex]) 
        {
            yield return new WaitForSecondsRealtime(timeBetweenCharacters);
            textField.text += character;
        }
        writingSentence = false;
    }

    private void EndDialog() 
    {
        onDialog = false;
        textField.text = "";
        Time.timeScale = 1;
        dialogHolder.SetActive(false);
    }
}
