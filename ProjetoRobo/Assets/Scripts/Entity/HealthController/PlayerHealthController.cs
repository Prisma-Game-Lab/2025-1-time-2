using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Componente Especifico que controla a vida do PLAYER
public class PlayerHealthController : EntityHealthController
{
    [SerializeField] private UnityEvent onDeathEvent;

    protected override void Die()
    {
        onDeathEvent?.Invoke();
    }
}
