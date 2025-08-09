using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private EnemyCont enemyController;

    [Header("Variables")]
    [SerializeField] private float distanceThreshold;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float accelerationRate;
    [SerializeField] private float dessaccelerationRate;

    private Transform targetTransform;
    private Vector2 targetPosition;
    private bool shouldMove;
    private bool usePosition;

    private void Start()
    {
        enemyController = GetComponent<EnemyCont>();
    }

    private void FixedUpdate()
    {
        if (shouldMove) ApplyForce();
    }

    public void SetTarget(Transform target) 
    {
        targetTransform = target;
        shouldMove = true;
        usePosition = false;
    }

    public void SetTarget(Vector2 targetPos)
    {
        targetPosition = targetPos;
        shouldMove = true;
        usePosition = true;
    }

    private void ApplyForce() 
    {
        Vector2 targetPos;

        if (usePosition) targetPos = targetPosition;
        else targetPos = targetTransform.position;

        Rigidbody2D rb = enemyController.rb;

        if (Vector2.Distance(targetPos, transform.position) < distanceThreshold)
        {
            rb.AddForce(-rb.velocity * dessaccelerationRate);
            return;
        }
            

        
        Vector2 direction = targetPos - (Vector2)transform.position;
        direction.Normalize();

        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(direction * accelerationRate);
        }
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
