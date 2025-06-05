using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnemyController : MonoBehaviour
{
    private bool foundPlayer = false;
    private Rigidbody2D rb;
    private Rigidbody2D player;

    [SerializeField] private float speed = 3f;
    [SerializeField] private float maxSpeed = 4f;
    [SerializeField] private float laserFireRate = 0.1f;
    [SerializeField] private float laserLifetime = 2f;
    [SerializeField] private float laserSpeed = 5f;
    [SerializeField] private LayerMask targetLayers;

    private float timer = 0f;
    private bool isAlive = true;

    [SerializeField] private LaserPoolController laserPool;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (foundPlayer && player != null)
        {
            Vector2 direction = (player.position - rb.position).normalized;
            if (rb.velocity.magnitude < maxSpeed)
                rb.AddForce(direction * speed);
            if (rb.velocity.magnitude > maxSpeed)
                rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (foundPlayer && timer >= 1/laserFireRate)
        {
            ShootLaser();
            timer = 0f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !foundPlayer)
        {
            foundPlayer = true;
            Debug.Log("Player triggered enemy!");
        }
    }

    void ShootLaser()
    {
        if (!isAlive || player == null || laserPool == null) return;

        GameObject laser = laserPool.GetLaser();
        if (laser != null)
        {
            laser.transform.position = transform.position;
            laser.SetActive(true);

            Laser laserScript = laser.GetComponent<Laser>();
            Vector2 direction = (player.position - rb.position).normalized;
            laserScript.SetUp(direction, laserSpeed, laserLifetime, targetLayers);
        }
    }
}
