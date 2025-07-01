using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollectible : CollectibleController
{
    
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerFiring pf = other.GetComponent<PlayerFiring>();
        pf?.IncreaseAmmoCount(1);
            

        AudioManager.Instance.PlaySFX("powerup_sfx");
        Debug.Log("Collected!");
        Destroy(gameObject);
    }

}
