using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    [SerializeField] private float switchTime;
    [SerializeField] private float warningTime;

    private float switchTimer;
    private float warningTimer;

    private InputManager inputManager; 

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
    if (player != null)
    {
        inputManager = player.GetComponent<InputManager>();
    }
    else
    {
        Debug.LogError("Player not found!");
    }
        switchTimer = switchTime;
        warningTimer = warningTime;

       
    }

    void Update()
    {
        if (inputManager == null) return; // prevent null reference errors

        switchTimer -= Time.deltaTime;
        if (switchTimer <= 0)
        {
            warningTimer -= Time.deltaTime;
            if (warningTimer <= 0)
            {
                inputManager.SwitchPlayerInput();
                switchTimer = switchTime;
                warningTimer = warningTime;
            }
        }
    }
}
