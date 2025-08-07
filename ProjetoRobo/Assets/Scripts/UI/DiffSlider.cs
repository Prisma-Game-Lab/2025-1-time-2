using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class DiffSlider : MonoBehaviour
{

    [SerializeField] TMP_Text displayText;

    [SerializeField] Slider slider;

    private GameManager GameManager;

    private void Start()
    {
        GameManager = FindAnyObjectByType<GameManager>();
        SyncSlider();
    }

    private void SyncSlider()
    {
        slider.value = GameManager.GetDifficulty();
        UpdateDisplayText(slider.value);
    }

    private void UpdateDisplayText(float value)
    {
        if (displayText != null)
        {
            switch (value)
            {
                case -1:
                    displayText.text = "Fácil";
                    break;
                case 0:
                    displayText.text = "Normal";
                    break;
                case 1:
                    displayText.text = "Difícil";
                    break;
                default:
                    displayText.text = "Erro!";
                    break;
            }
        }
    }

    public void OnChangeSlider(float Value)
    {
        UpdateDisplayText(Value);
        GameManager.SetDifficulty(Value);
    }
}
