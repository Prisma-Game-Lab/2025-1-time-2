using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : CollectibleController
{
     protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        EntityHealthController ehc = other.GetComponent<EntityHealthController>();
        ehc?.Heal(1);
                
        AudioManager.Instance.PlaySFX("powerup_sfx");
        Debug.Log("Collected!");
        Destroy(gameObject);
    }
}
