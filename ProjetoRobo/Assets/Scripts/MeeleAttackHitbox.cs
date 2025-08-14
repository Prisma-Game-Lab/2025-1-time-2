using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleAttackHitbox : MonoBehaviour
{
    public int MeleeDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageController damageController = collision.GetComponent<DamageController>();
       
       
        var enemyHealth = collision.GetComponentInParent<EnemyHealthController>();
        if (enemyHealth != null)
        {
            enemyHealth.SetPendingDamageType(EnemyHealthController.DamageType.Melee);
        }
        if (damageController != null)
        {
            
            damageController.OnDamage(MeleeDamage);
        }
        
    }
}
