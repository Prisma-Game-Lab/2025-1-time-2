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

    public GameObject healthDropPrefab;
    public GameObject ammoDropPrefab;


    [SerializeField] public int maxHealth;

    [SerializeField] public float dropChance;

    public int currentHealth;



    private UnityEvent OnDamage;
    private UnityEvent OnHeal;

    public bool isEnemy;

    private void Start()
    {
        
        ec = GetComponent<EntityController>();

        currentHealth = maxHealth;
    }

    public void TakeDamage() 
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
    void Drop(){
    Random.InitState(System.DateTime.Now.Millisecond);
    float rand1 = Random.Range(0.0f, 1.0f);
        if (rand1 <= dropChance)
        {
            int rand2 = Random.Range(0, 2);
            if (rand2 == 0)
            {
                Instantiate(healthDropPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(ammoDropPrefab, transform.position, Quaternion.identity);
            }
        }
        
    }
    


    protected virtual void Die() 
    {
        if (isEnemy == true)
        {
            UIManager uim = FindObjectOfType<UIManager>();
            uim.pontos++;
            AudioManager.Instance.PlaySFX("enemy_death_sfx");
            Drop();
        }
        AudioManager.Instance.PlaySFX("player_death_sfx");
        Destroy(gameObject);
    }
}
