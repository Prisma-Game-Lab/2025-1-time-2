using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAI : MonoBehaviour
{
    private EnemyCont enemyController;

    private Transform target;

    [SerializeField] private float desiredDistanceFromTarget;

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
            Vector2 targetWithOffset = (Vector2)target.transform.position - dirVector.normalized * desiredDistanceFromTarget;
            enemyController.enemyMovement.SetTarget(targetWithOffset);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = collision.transform;
            enemyController.enemyFiring.SetTarget(target);
        }
    }
}
