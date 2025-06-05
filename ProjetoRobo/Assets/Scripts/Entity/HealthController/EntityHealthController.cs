using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Componente Generico que controla a vida da entidade
public class EntityHealthController : MonoBehaviour
{
    //Reference to controller
    private EntityController ec;

    [SerializeField] private int maxHealth;

    private int currentHealth;

    private UnityEvent OnDamage;
    private UnityEvent OnHeal;

    private void Start()
    {
        ec = GetComponent<EntityController>();

        currentHealth = maxHealth;
    }

    public void TakeDamage() 
    {
        //Nao podemos passar o dano por parametro, ja que DamageController é generico e nao passa parametros 
        //(Para poder ser utilizado em objetos que não tem esse componente)
        //Logo pegamos diretamento o valor do dano

        currentHealth -= ec.damageController.damage;

        OnDamage?.Invoke();

        if (currentHealth <= 0) 
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth) 
        {
            currentHealth = maxHealth;
        }

        OnHeal?.Invoke();
    }

    protected virtual void Die() 
    {
        Destroy(gameObject);
    }
}
