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
    [SerializeField] public float maxVariableTimer;
    [SerializeField] public float maxFixedTimer;

    [SerializeField] public float onDamageDecrease;

    [SerializeField] private UnityEvent onInputChange;

    [SerializeField] private float sfxVolume;

    private bool onTransition;
    public float currentVariableTimer;
    public float currentFixedTimer;

    private AudioSource loopedSFXSource;
   
    void Start()
    {
        loopedSFXSource = gameObject.AddComponent<AudioSource>();
        loopedSFXSource.clip = Array.Find(AudioManager.Instance.sfxSounds, x => x.name == "GlitchDistortion");
        loopedSFXSource.loop = true;
        loopedSFXSource.outputAudioMixerGroup = AudioManager.Instance.sfxMixerGroup;
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
        }
        else 
        {
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

        pc.playerUI.UpdateBars((currentVariableTimer + currentFixedTimer) / (maxFixedTimer + maxVariableTimer));
    }

    private void ActivateTransition() 
    {
        //cameraAnim.SetTrigger("Shake");

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
        if (timerPaused) return;

        bool activateSlowFill = true;

        if (currentVariableTimer <= 0)
        {
            activateSlowFill = false;
        }

        currentVariableTimer -= amount;

        if (currentVariableTimer < 0) 
        {
            currentVariableTimer = 0;
        }

        pc.playerUI.UpdateBars((currentVariableTimer + currentFixedTimer) / (maxFixedTimer + maxVariableTimer), activateSlowFill);
    }
}
