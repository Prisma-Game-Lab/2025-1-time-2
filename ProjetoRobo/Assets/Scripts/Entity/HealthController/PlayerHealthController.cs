using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Componente Especifico que controla a vida do PLAYER
public class PlayerHealthController : EntityHealthController
{
    [SerializeField] private UnityEvent onDeathEvent;
    private PlayerParry playerParry;
    protected override void TakeDamage()
    {
        playerParry = GetComponent<PlayerParry>();
       
        if (playerParry != null && playerParry.IsParrying)
        {
            playerParry.SuccessfulParry();
            Debug.Log("Parry!");
            return;
        }

        if (playerParry.IsInvulnerable)
        {
            Debug.Log("Player is Invulnerable!");
            return;
        }

        base.TakeDamage();
    }
    protected override void Die()
    {

        onDeathEvent?.Invoke();
    }
}
