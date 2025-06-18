using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{


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
