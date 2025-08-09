using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyPoolController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemysPrefab;

    [SerializeField]
    private int initialPoolSize = 10;
    private WaveController waveController;
    private SpawnManager spawnManager;

    private List<GameObject> enemyPool;

    public UnityEvent isEnemiesDead;

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

    public GameObject GetEnemy(Vector2 spawnPos)
    {
        foreach (GameObject enemy in enemyPool)
        {
            if (!enemy.activeInHierarchy) 
            {
                enemy.transform.position = spawnPos;
                enemy.SetActive(true);
                return enemy;
            }
        }

        // If all lasers are in use, expand the pool
        return AddNewEnemyToPool(spawnPos);
    }

    private GameObject AddNewEnemyToPool(Vector2 spawnPos)
    {
        int randomInt = Random.Range(0, enemysPrefab.Length);
        GameObject newEnemy = Instantiate(enemysPrefab[randomInt], spawnPos, Quaternion.identity);

        newEnemy.SetActive(true);
        enemyPool.Add(newEnemy);
        return newEnemy;
    }
}
