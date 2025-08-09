using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class WaveController : MonoBehaviour
{
    private static int currentWave;

    [SerializeField]
    private int perWaveEnemyCount;

    [SerializeField]
    private EnemyPoolController enemyPool;
    private int _enemiesDestroyed;
    private bool isEnemiesDead;

    [SerializeField]
    private TextMeshProUGUI waveInfoText;

    [SerializeField]
    private SpawnManager spawnManager;

    public UnityEvent OnWavePassed;

    // Start is called before the first frame update
    void Start()
    {
        currentWave = 0;
        _enemiesDestroyed = 0;
        isEnemiesDead = false;
        waveInfoText.text = "Wave: " + currentWave;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnemiesDead)
        {
            NextWave();
            isEnemiesDead = false;
        }
    }

    //Get the number of enemies to be increased after completing wave
    public int GetPWEnemyCount()
    {
        return perWaveEnemyCount * currentWave;
    }

    public void NextWave()
    {
        Debug.Log("Next Wave!");
        currentWave++;
        waveInfoText.text = "Wave: " + currentWave;
        OnWavePassed?.Invoke();
    }
}
