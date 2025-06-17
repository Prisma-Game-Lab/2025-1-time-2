using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private WaveController waveController;

    [SerializeField]
    private EnemyPoolController enemyPoolController;

    [SerializeField]
    private BoxCollider2D spawnZone;

    [SerializeField]
    private float spawnPadding = 1f;

    // Start is called before the first frame update
    void Start()
    {
        waveController.OnWavePassed += HandleWavePassed;
    }

    // Update is called once per frame
    void Update() { }

    private void HandleWavePassed()
    {
        enemyPoolController.AddNewEnemyToPool(GetRandomScreenPosition());
    }

    public Vector2 GetRandomScreenPosition()
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
}
