using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Componente (extremamente) Generico que controla quando a entidade toma dano
public class DamageController : MonoBehaviour
{
    [SerializeField] private UnityEvent DamageEvent;

    [HideInInspector] public int damage;

    public void OnDamage(int damageTaken) 
    {
        damage = damageTaken;

        DamageEvent?.Invoke();
    }
}
