using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public TMP_Text pontos;

    public void SetPoints()
    {
        UIManager uim = FindObjectOfType<UIManager>();
        pontos.text = uim.pontos.ToString() + " Pontos";
    }

    public void OnRetry() 
    {
        LevelManager.RestartLevel();
    }
    
    public void Menu()
    {
        AudioManager.Instance.StopMusic(); 
        AudioManager.Instance.PlayMusic("menu_music");
        LevelManager.LoadSceneByName("MainMenu");
    }
}
