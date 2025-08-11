using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleEnemyAI : MonoBehaviour
{
    private EnemyCont enemyController;
    private Transform target;
    [SerializeField] private float minDistanceAttack;

    private void Start()
    {
        enemyController = GetComponent<EnemyCont>();
    }

    private void FixedUpdate()
    {
        if (target != null) 
        {
            Vector2 dirVector = target.position - transform.position;
            enemyController.enemyRotation.SetDesiredRotation(dirVector);

            float distance = dirVector.magnitude;

            if (distance < minDistanceAttack) 
            {
                enemyController.enemyMeele.PerformMeeleAttack();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = collision.transform;
            enemyController.enemyMovement.SetTarget(collision.transform);
        }
    }
}
