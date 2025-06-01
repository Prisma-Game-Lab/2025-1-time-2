using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController
{
    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public InputManager inputManager;

    [HideInInspector] public PlayerParry playerParry;

    protected override void Awake()
    {
        base.Awake();
        
        playerMovement = GetComponent<PlayerMovement>();
        inputManager = GetComponent<InputManager>();

        playerParry = GetComponent<PlayerParry>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
         playerParry?.AttemptParry(other.gameObject);
    }
}


