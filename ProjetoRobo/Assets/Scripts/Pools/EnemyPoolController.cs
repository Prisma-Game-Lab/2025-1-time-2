using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyPoolController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int initialPoolSize = 10;
    private WaveController waveController;

    private List<GameObject> enemyPool;
    [SerializeField] private BoxCollider2D spawnZone;
    [SerializeField] private float spawnPadding = 1f;

    void Start()
    {
        waveController = GetComponent<WaveController>();
        initialPoolSize += waveController.GetPWEnemyCount();
        enemyPool = new List<GameObject>();

        for (int i = 0; i < initialPoolSize; i++)
        {
            AddNewEnemyToPool();
        }
    }


    private Vector2 GetRandomScreenPosition()
    {
        Bounds bounds = spawnZone.bounds;
        Vector2 min = bounds.min;
        Vector2 max = bounds.max;

        Vector2 camMin = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 camMax = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        Vector2 spawnPos;

        int maxAttempts = 100;
        int attempts = 0;
    
        do
        {
            float x = Random.Range(min.x + spawnPadding, max.x - spawnPadding);
            float y = Random.Range(min.y + spawnPadding, max.y - spawnPadding);
            spawnPos = new Vector2(x, y);
            attempts++;
        } while (IsInsideCamera(spawnPos) && attempts < maxAttempts);

        return spawnPos;
    }

    private bool IsInsideCamera(Vector2 pos)
    {
        Vector2 camMin = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 camMax = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        return pos.x >= camMin.x && pos.x <= camMax.x && pos.y >= camMin.y && pos.y <= camMax.y;
    }



    private GameObject AddNewEnemyToPool()
    {
        Vector2 spawnPosition = GetRandomScreenPosition();
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        newEnemy.SetActive(true);
        enemyPool.Add(newEnemy);
        return newEnemy;
    }
}
