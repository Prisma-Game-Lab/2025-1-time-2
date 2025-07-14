using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject dialogHolder;
    [SerializeField] private TMP_Text textField;
    [SerializeField] private Image imageField;

    [Header("Variables")]
    [SerializeField] private float timeBetweenCharacters;
    [SerializeField] private Sprite defaultSprite;

    private DialogData currentDialogData;
    private int currentDialogIndex;
    private string currentDialogText;
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
        currentDialogText = currentDialogData.dialogueLines[currentDialogIndex].DialogText;

        ChangeSpeakerImage();
        
        Time.timeScale = 0;
        writeSentenceCoroutine = StartCoroutine(WriteSentence());
    }

    public void AdvanceDialog() 
    {
        if (writingSentence) 
        {
            StopCoroutine(writeSentenceCoroutine);
            textField.text = currentDialogText;
            writingSentence = false;
            return;
        }

        currentDialogIndex++;
        if (currentDialogIndex >= currentDialogData.dialogueLines.Length)
        {
            EndDialog();
            return;
        }
        currentDialogText = currentDialogData.dialogueLines[currentDialogIndex].DialogText;
        ChangeSpeakerImage();

        writeSentenceCoroutine = StartCoroutine(WriteSentence());
    }

    private IEnumerator WriteSentence()
    {
        writingSentence = true;
        textField.text = "";
        foreach (char character in currentDialogText)
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

    private void ChangeSpeakerImage() 
    {
        Sprite currentSpeakerSprite = currentDialogData.dialogueLines[currentDialogIndex].SpeakerSprite;

        if (currentSpeakerSprite == null)
        {
            imageField.sprite = defaultSprite;
        }
        else
        {
            imageField.sprite = currentSpeakerSprite;
        }
    }
}
