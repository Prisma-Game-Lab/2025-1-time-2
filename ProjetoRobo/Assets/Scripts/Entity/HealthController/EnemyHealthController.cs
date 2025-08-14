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

    public enum DamageType { Melee, Shot, Other,Unknown }
    private DamageType pendingDamageType = DamageType.Shot;

    


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
    /* private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("Melee") || collision.transform.root.CompareTag("Melee"))
        {
            lastDamageType = DamageType.Melee;
            return;
        }
        else
        {
            lastDamageType = DamageType.Shot;
            return;
        }

    }
    */

    
    public void SetPendingDamageType(DamageType type)
    {
        pendingDamageType = type;
    }

    protected override void TakeDamage()
    {
        if ((pendingDamageType == DamageType.Melee && immuneToMelee) ||
            (pendingDamageType == DamageType.Shot && immuneToShot))
        {
            Debug.Log($"Blocked {pendingDamageType} damage!");
            pendingDamageType = DamageType.Shot; 
            return;
        }

        base.TakeDamage();
        pendingDamageType = DamageType.Shot; 
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

  

   
