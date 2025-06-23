using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTimer : MonoBehaviour
{
    //Reference to controller
    private PlayerController pc;

    public Animator cameraAnim;

    [Header("Timer Configurations")]
    [SerializeField] private bool timerPaused;
    [SerializeField] private float maxVariableTimer;
    [SerializeField] private float maxFixedTimer;

    [SerializeField] public float onDamageDecrease;

    [SerializeField] private UnityEvent onInputChange;

    [SerializeField] private float sfxVolume;

    private bool onTransition;
    private float currentVariableTimer;
    private float currentFixedTimer;

    private AudioSource loopedSFXSource;
   
    void Start()
    {
        loopedSFXSource = gameObject.AddComponent<AudioSource>();
        loopedSFXSource.clip = Array.Find(AudioManager.Instance.sfxSounds, x => x.name == "switch_warning_sfx");
        loopedSFXSource.loop = true;
        loopedSFXSource.volume = sfxVolume;
        pc = GetComponent<PlayerController>();
        

        currentVariableTimer = maxVariableTimer;
        currentFixedTimer = maxFixedTimer;
    }

    private void FixedUpdate()
    {
        if (!timerPaused) 
        {
            UpdateTimer();
        }
    }

    private void UpdateTimer() 
    {
        if (currentVariableTimer > 0) 
        {
            currentVariableTimer -= Time.deltaTime;
            return;
        }

        if (!onTransition) 
        {
            ActivateTransition();
            onTransition = true;
        }

        currentFixedTimer -= Time.deltaTime;
        if (currentFixedTimer < 0) 
        {
            ActivateChange();
        }
    }

    private void ActivateTransition() 
    {
        if (loopedSFXSource != null && !loopedSFXSource.isPlaying)
        loopedSFXSource.Play();
        print("O robo ira trocar de input");
    }

    private void ActivateChange() 
    {
         //CrosshairVisual visual = GetComponent<CrosshairVisual>();



        currentVariableTimer = maxVariableTimer;
        currentFixedTimer = maxFixedTimer;
        onTransition = false;
        
        if (loopedSFXSource != null && loopedSFXSource.isPlaying)
            loopedSFXSource.Stop();

        cameraAnim.SetTrigger("Shake");

        AudioManager.Instance.PlaySFX("switch_sfx");
        pc.inputManager.SwitchPlayerInput();
        pc.playerFiring.Crosshair.SwitchSprite();
        onInputChange?.Invoke();
    }

    public void DecreaseTimer(float amount) 
    {
        currentVariableTimer -= amount;
    }
}
