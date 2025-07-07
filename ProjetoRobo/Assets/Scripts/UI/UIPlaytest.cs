using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIPlaytest : MonoBehaviour
{
    [SerializeField] string[] text = new string[4];
    [SerializeField] TMP_Text textfield;

    private void Start()
    {
        UpdateUI();
    }

    public void increaseConter(InputAction.CallbackContext cont) 
    {
        if (cont.performed) 
        {
            GameManager.Instance.mortarShotConfiguration = (GameManager.Instance.mortarShotConfiguration + 1)%4;
            UpdateUI();
        }
    }

    public void decreaseConter(InputAction.CallbackContext cont)
    {
        if (cont.performed)
        {
            GameManager.Instance.mortarShotConfiguration = GameManager.Instance.mortarShotConfiguration - 1;
            if (GameManager.Instance.mortarShotConfiguration < 0) 
            {
                GameManager.Instance.mortarShotConfiguration = 3;
            }
            UpdateUI();
        }
    }

    private void UpdateUI() 
    {
        textfield.text = text[GameManager.Instance.mortarShotConfiguration];
    } 
}
