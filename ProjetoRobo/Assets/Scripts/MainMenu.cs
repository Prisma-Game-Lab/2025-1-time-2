using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public string sceneName;
    void Start()
    {
        if (AudioManager.Instance != null)
        {
        AudioManager.Instance.PlayMusic("megaWall");
        }
    }
    public void Play()
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
