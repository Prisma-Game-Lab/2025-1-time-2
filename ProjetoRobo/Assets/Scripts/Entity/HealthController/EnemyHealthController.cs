using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EnemyHealthController : EntityHealthController
{


    [Header("Immunities")]
    public bool immuneToMelee;
    public bool immuneToShot;
    
    [Header("Prefabs")]
    public GameObject healthDropPrefab;
    public GameObject ammoDropPrefab;

    [SerializeField] private GameObject shieldPrefab;

    [SerializeField] private float dropChance;

    [SerializeField] private int score;
    [SerializeField] private UnityEvent onDeathEvent;

    public enum DamageType { Melee, Shot, Other }
    public DamageType lastDamageType;

    


    protected override void Start()
    {
        base.Start();
        if (!immuneToShot && !immuneToMelee)
        {
            return;
        }
        
        GameObject immunityShield = Instantiate(shieldPrefab, transform.position, Quaternion.identity, transform);
        EnemyShieldVisual shieldVisual = immunityShield.GetComponent<EnemyShieldVisual>();
        if (immuneToMelee)
        {
            
            shieldVisual.ShotShield();
        }
        else if (immuneToShot)
        {
            shieldVisual.MeleeShield();
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("Melee"))
        {
            lastDamageType = DamageType.Melee;
        }
        else if (collision.CompareTag("ReflectedLaser"))
        {
            lastDamageType = DamageType.Other;
        }
        else
        {
            lastDamageType = DamageType.Shot;
        }

    }

    
    protected override void TakeDamage()
    {
        if ((lastDamageType == DamageType.Melee && immuneToMelee) ||
        (lastDamageType == DamageType.Shot && immuneToShot))
        {
        Debug.Log($"Blocked {lastDamageType} damage!");
        return;
        }

        base.TakeDamage();
    }
    public void Drop()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        float rand1 = Random.Range(0.00f, 1.0f);
        if (rand1 < dropChance)
        {
            int rand2 = Random.Range(0, 2);
            if (rand2 == 0)
            {
                Instantiate(healthDropPrefab, transform.position, Quaternion.identity);
            }
            if(rand2 == 1)
            {
                Instantiate(ammoDropPrefab, transform.position, Quaternion.identity);
            }
        }

    }

    public void IncreaseScore()
    {
        UIManager uim = FindObjectOfType<UIManager>();
        uim.pontos += score;
    }

    protected override void Die()
    {

        onDeathEvent?.Invoke();
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }
}

  

   
