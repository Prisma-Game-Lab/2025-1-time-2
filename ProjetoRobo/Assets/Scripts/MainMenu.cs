using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public string sceneName;
    [SerializeField] private string menuMusic;

    [SerializeField] private string combatMusic;
   
    void Start()
    {
        
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayMusic(menuMusic);
        }
    }
    public void Play()
    {
        AudioManager.Instance.PlayMusic(combatMusic);
        SceneManager.LoadSceneAsync(sceneName);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
