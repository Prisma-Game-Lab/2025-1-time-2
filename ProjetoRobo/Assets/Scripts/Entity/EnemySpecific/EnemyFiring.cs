using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFiring : MonoBehaviour
{
    private EnemyCont enemyController;
    private Transform target;
    private LaserPoolController laserPool;

    [SerializeField] private int laserDamage;
    [SerializeField] private float laserFireRate = 0.1f;
    [SerializeField] private float laserLifetime = 2f;
    [SerializeField] private float laserSpeed = 5f;
    [SerializeField] private LayerMask targetLayers;
    [SerializeField] private float maxFiringDistance;

    private bool shouldShoot = false;
    private float firingCooldown = 0;

    private void Start()
    {
        enemyController = GetComponent<EnemyCont>();
        laserPool = GameManager.Instance.GetLaserPoolController();
    }

    private void FixedUpdate()
    {
        if (firingCooldown > 0) 
        {
            firingCooldown -= Time.deltaTime;
        }

        if (shouldShoot) 
        {
            if (firingCooldown <= 0 && Vector2.Distance(target.position, transform.position) < maxFiringDistance)
            {
                ShootLaser();
            }
        }
    }

    public void SetTarget(Transform newTarget) 
    {
        target = newTarget;
        shouldShoot = target != null;
    }

    private void ShootLaser()
    {
        if (laserPool == null) return;

        GameObject laser = laserPool.GetLaser();
        if (laser == null) return;

        laser.transform.position = transform.position;
        laser.SetActive(true);

        Laser laserScript = laser.GetComponent<Laser>();
        Vector2 direction = (target.position - transform.position).normalized;
        laserScript.SetUp(direction, laserDamage, laserSpeed, laserLifetime, targetLayers);
        firingCooldown = 1 / laserFireRate;
        AudioManager.Instance.PlaySFX("enemy_laser_sfx");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, maxFiringDistance);
    }
}
