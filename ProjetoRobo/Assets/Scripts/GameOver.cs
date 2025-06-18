using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TMP_Text pontos;


    public void SetPoints()
    {
         UIManager uim = FindObjectOfType<UIManager>();
        pontos.text = uim.pontos.ToString() + " Pontos";
    }
    public void Play(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
    
    public void Menu()
{
    AudioManager.Instance.StopMusic(); 
    AudioManager.Instance.PlayMusic("menu_music");
    SceneManager.LoadScene("MainMenu");
}


    
    
}
