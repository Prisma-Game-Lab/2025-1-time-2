using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [SerializeField] public string sceneName;

    [SerializeField] private AudioMixer Mixer;
    [SerializeField] private string menuMusic;

    [SerializeField] private string combatMusic;
   
    void Start()
    {
        Mixer.SetFloat("Volume", Mathf.Log10(0.5f) * 20);
        Mixer.SetFloat("SFXVolume", Mathf.Log10(0.5f) * 20);
        Mixer.SetFloat("MusicVolume", Mathf.Log10(0.5f) * 20);
        
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
