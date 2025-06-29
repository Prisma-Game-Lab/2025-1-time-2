using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMusic : MonoBehaviour
{
    public Button toggleMusicButton;
    [SerializeField] private string alternateSongName; 

    void Start()
    {
        if (toggleMusicButton != null)
        {
            toggleMusicButton.onClick.AddListener(() =>
            {
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.ToggleMusic(alternateSongName);
                }
            });
        }
    }
}
