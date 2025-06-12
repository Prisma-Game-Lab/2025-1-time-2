using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController
{
    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public InputManager inputManager;
    [HideInInspector] public PlayerFiring playerFiring;
    [HideInInspector] public PlayerTimer playerTimer;

    [HideInInspector] public PlayerParry playerParry;

    public GameObject playerTracks;
    public GameObject playerBody;

    protected override void Awake()
    {
        base.Awake();
        
        playerMovement = GetComponent<PlayerMovement>();
        inputManager = GetComponent<InputManager>();
        playerFiring = GetComponent<PlayerFiring>();
        playerTimer = GetComponent<PlayerTimer>();

        playerParry = GetComponent<PlayerParry>();
    }

    //Nao deveria estar aqui
    //Falar com o alvarenga
    
}


