using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Events;

//Componente Generico que controla a vida da entidade
public class EntityHealthController : MonoBehaviour
{
    //Reference to controller
    private EntityController ec;


    [SerializeField] public int maxHealth;

    [SerializeField] private string deathSfx;


    public int currentHealth;

    private UnityEvent OnDamage;
    private UnityEvent OnHeal;

    private void Start()
    {
        
        ec = GetComponent<EntityController>();

        currentHealth = maxHealth;
    }

    protected virtual void TakeDamage()
    {
        //Nao podemos passar o dano por parametro, ja que DamageController � generico e nao passa parametros 
        //(Para poder ser utilizado em objetos que n�o tem esse componente)
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

    public void PlayDeathSfx()
    {
        AudioManager.Instance.PlaySFX(deathSfx);
    }


    protected virtual void Die() 
    {
        
        Destroy(gameObject);
    }
}
