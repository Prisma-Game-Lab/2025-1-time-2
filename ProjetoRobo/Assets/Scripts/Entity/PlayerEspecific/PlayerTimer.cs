using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTimer : MonoBehaviour
{
    //Reference to controller
    private PlayerController pc;

    [Header("Timer Configurations")]
    [SerializeField] private bool timerPaused;
    [SerializeField] private float maxVariableTimer;
    [SerializeField] private float maxFixedTimer;

    [SerializeField] private UnityEvent onInputChange;

    private bool onTransition;
    private float currentVariableTimer;
    private float currentFixedTimer;

    void Start()
    {
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
        print("O robo ira trocar de input");
    }

    private void ActivateChange() 
    {
        currentVariableTimer = maxVariableTimer;
        currentFixedTimer = maxFixedTimer;
        onTransition = false;

        pc.inputManager.SwitchPlayerInput();
        onInputChange?.Invoke();
    }

    public void DecreaseTimer(float amount) 
    {
        currentVariableTimer -= amount;
    }
}
