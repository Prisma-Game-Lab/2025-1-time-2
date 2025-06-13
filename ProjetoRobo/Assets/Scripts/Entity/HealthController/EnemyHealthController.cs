using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EnemyHealthController : EntityHealthController
{
    public GameObject healthDropPrefab;
    public GameObject ammoDropPrefab;

    [SerializeField] private float dropChance;

    [SerializeField] private int score;
    [SerializeField] private UnityEvent onDeathEvent;
    


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
}

  

   
