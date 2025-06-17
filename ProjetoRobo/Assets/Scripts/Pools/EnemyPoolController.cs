using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyPoolController : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private int initialPoolSize = 10;
    private WaveController waveController;
    private SpawnManager spawnManager;

    private List<GameObject> enemyPool;

    public event Action isEnemiesDead;

    void Start()
    {
        spawnManager = GetComponent<SpawnManager>();
        waveController = GetComponent<WaveController>();
        initialPoolSize += waveController.GetPWEnemyCount();
        enemyPool = new List<GameObject>();

        for (int i = 0; i < initialPoolSize; i++)
        {
            AddNewEnemyToPool(spawnManager.GetRandomScreenPosition());
        }
    }

    void Update()
    {
        foreach (GameObject enemy in enemyPool)
        {
            if (enemy.activeInHierarchy)
            {
                return;
            }
        }

        Debug.Log("All enemies are dead!");
        isEnemiesDead?.Invoke();
    }

    public GameObject AddNewEnemyToPool(Vector2 spawnPos)
    {
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        newEnemy.SetActive(true);
        enemyPool.Add(newEnemy);
        return newEnemy;
    }
}
