using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using UnityEngine;

public class ShootParryPowerup : PowerUpController
{
    private PlayerParry pp;
    private PlayerFiring pf;

    [Header("Buff Configuration")]
    [SerializeField] private int damageIncrease;

    [Header("Nerf Configuration")]

    [SerializeField] private float parryWindowDecrease;

    [SerializeField] private int bulletsLost;
    
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        pp = other.GetComponent<PlayerParry>();
        pf = other.GetComponent<PlayerFiring>();
        AudioManager.Instance.PlaySFX("powerup_sfx");
        Debug.Log("Collected!");
        cantDespawn = true;
        StartCoroutine(BuffSequence());


    }

    protected override void ActivateBuff()
    {
        pp.ReflectableParry();
        pf.IncreaseDamage(damageIncrease);

    }

    protected override void DeactivateBuff()
    {
        pp.ReflectableParry();
        pf.IncreaseDamage(-damageIncrease);
        
    }
    protected override void ActivateNerf()
    {
        pp.ChangeParryWindow(-parryWindowDecrease);
        pf.IncreaseAmmoCount(-bulletsLost);
    }
    protected override void DeactivateNerf()
    {
        pp.ChangeParryWindow(parryWindowDecrease);
       
        Destroy(gameObject);
    }

}
