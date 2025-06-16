using System.Collections.Generic;
using UnityEngine;

public class LaserPoolController : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private int initialPoolSize = 10;
    [SerializeField] private int minPoolSize;
    [SerializeField] private float maxTimeUnused;

    private List<GameObject> laserPool;
    private int currentLaserPoolSize; 
    private float currentTimeUnused;

    void Start()
    {
        laserPool = new List<GameObject>();

        for (int i = 0; i < initialPoolSize; i++)
        {
            AddNewLaserToPool();
        }
    }

    public GameObject GetLaser()
    {
        currentTimeUnused = 0;

        foreach (GameObject laser in laserPool)
        {
            if (!laser.activeInHierarchy)
                return laser;
        }

        // If all lasers are in use, expand the pool
        return AddNewLaserToPool();
    }

    private void FixedUpdate()
    {
        RemoveLaserTimer();
    }

    private GameObject AddNewLaserToPool()
    {
        GameObject newLaser = Instantiate(laserPrefab, transform.position, Quaternion.identity, transform);
        newLaser.SetActive(false);
        laserPool.Add(newLaser);
        currentLaserPoolSize += 1;
        return newLaser;
    }

    private void RemoveLaserTimer()
    {
        if (currentLaserPoolSize > minPoolSize) 
        {
            currentTimeUnused += Time.deltaTime;
            if (currentTimeUnused > maxTimeUnused)
            {
                RemoveLaserFromPool();
                currentTimeUnused = 0;
            }
        }
        else 
        {
            currentTimeUnused = 0;
        }
    }

    private void RemoveLaserFromPool()
    {
        foreach (GameObject laser in laserPool) 
        {
            if (laser.activeInHierarchy == false) 
            {
                laserPool.Remove(laser);
                Destroy(laser);
                currentLaserPoolSize -= 1;
                break;
            }
        }
    }
}
