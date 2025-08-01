using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Componente Especifico que controla a vida do PLAYER
public class PlayerHealthController : EntityHealthController
{
    //Reference to controller
    private PlayerController pc;

    [SerializeField] private UnityEvent onDeathEvent;

    [Header("I-Frames Variables")]
    [SerializeField] private float iFramesOnHit;
    [SerializeField] private float initialIFrames;
    private float currentIFrames;

    protected override void Start()
    {
        base.Start();

        pc = GetComponent<PlayerController>();
        currentIFrames = initialIFrames;
    }

    private void FixedUpdate()
    {
        if (currentIFrames > 0)
        {
            currentIFrames -= Time.deltaTime;
            if (currentIFrames <= 0)
            {
                pc.playerBodyFlicker.DeactivateFlicker();
                pc.playerTracksFlicker.DeactivateFlicker();
            }
        }
    }

    protected override void TakeDamage()
    {
        //pc.playerParry.OnHit();

        if (currentIFrames > 0) 
        {
            return;
        }

        base.TakeDamage();
        pc.playerTimer.DecreaseTimer(pc.playerTimer.onDamageDecrease);
        AudioManager.Instance?.PlaySFX("hit_sfx");
        pc.playerBodyFlash.ActivateFlash();
        pc.playerTracksFlash.ActivateFlash();
        pc.playerBodyFlicker.ActivateFlicker();
        pc.playerTracksFlicker.ActivateFlicker();
        SetIFrames(iFramesOnHit);
    }

    public void SetIFrames(float desiredIFrames) 
    {
        currentIFrames = desiredIFrames;
    }

    protected override void Die()
    {
        onDeathEvent?.Invoke();
    }
}
