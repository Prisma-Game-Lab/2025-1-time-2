using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController
{
    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public InputManager inputManager;
    [HideInInspector] public PlayerFiring playerFiring;
    [HideInInspector] public PlayerTimer playerTimer;
    [HideInInspector] public PlayerUI playerUI;
    [HideInInspector] public PlayerHealthController playerHealthController;
    public PlayerBodyRotation playerBodyRotation;
    public FlashEffectScript playerBodyFlash;
    public FlickerEffectScript playerBodyFlicker;
    public PlayerBodyRotation playerTracksRotation;
    public FlashEffectScript playerTracksFlash;
    public FlickerEffectScript playerTracksFlicker;

    [HideInInspector] public PlayerParry playerParry;

    protected override void Awake()
    {
        base.Awake();
        
        playerMovement = GetComponent<PlayerMovement>();
        inputManager = GetComponent<InputManager>();
        playerFiring = GetComponent<PlayerFiring>();
        playerTimer = GetComponent<PlayerTimer>();
        playerUI = GetComponent<PlayerUI>();
        playerHealthController = GetComponent<PlayerHealthController>();

        playerParry = GetComponent<PlayerParry>();
    }   
}


