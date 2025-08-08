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
    public FlashEffectScript playerBodyGreenFlash;
    public FlickerEffectScript playerBodyFlicker;
    public PlayerBodyRotation playerTracksRotation;
    public FlashEffectScript playerTracksFlash;
    public FlashEffectScript playerTracksGreenFlash;
    public FlickerEffectScript playerTracksFlicker;

    [HideInInspector] public PlayerParry playerParry;

    [Header("Player Skills")]
    public bool defensiveActionUnlocked;
    public bool offensiveActionUnlocked;
    public bool defensiveMorphUnlocked;
    public bool offensiveMorphUnlocked;

    public bool defensiveActionBlocked { get; private set; }
    public bool offensiveActionBlocked { get; private set; }
    public bool defensiveMorphBlocked { get; private set; }
    public bool offensiveMorphBlocked { get; private set; }

    private void OnEnable()
    {
        GameManager.Instance?.OnPause.AddListener(SetSkillsBlock);
    }

    private void OnDisable()
    {
        GameManager.Instance?.OnPause.RemoveListener(SetSkillsBlock);
    }

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

    public void SetSkillsBlock(bool state)
    {
        defensiveActionBlocked = state;
        offensiveActionBlocked = state;
        defensiveMorphBlocked = state;
        offensiveMorphBlocked = state;
    }
    public void SetDefensiveMorph(bool state)
    {
        defensiveMorphUnlocked = state;
    }

    public void SetOffensiveMorph(bool state)
    {
        offensiveMorphUnlocked = state;
    }

    public void SetMorphs(bool state)
    {
        SetDefensiveMorph(state);
        SetOffensiveMorph(state);
    }

    public void SetDefensiveAction(bool state)
    {
        defensiveActionUnlocked = state;
    }
    
    public void SetOffensiveAction(bool state)
    {
        offensiveActionUnlocked = state;
    }
}


