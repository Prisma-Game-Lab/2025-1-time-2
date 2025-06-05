using System.Collections.Generic;
using UnityEngine;

public class LaserPoolController : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private int initialPoolSize = 10;

    private List<GameObject> laserPool;

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
        foreach (GameObject laser in laserPool)
        {
            if (!laser.activeInHierarchy)
                return laser;
        }

        // If all lasers are in use, expand the pool
        return AddNewLaserToPool();
    }

    private GameObject AddNewLaserToPool()
    {
        GameObject newLaser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        newLaser.SetActive(false);
        //newLaser.transform.parent = transform;
        laserPool.Add(newLaser);
        return newLaser;
    }
}
