using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    private static int currentWave;

    [SerializeField] private int perWaveEnemyCount;
    [SerializeField] private EnemyPoolController enemyPool;
    private bool isEnemiesDead;

    [SerializeField] private Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        currentWave = 0;
        isEnemiesDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isEnemiesDead){
            currentWave++;
        }
    }

    //Get the number of enemies to be increased after completing wave
    public int GetPWEnemyCount(){
        return perWaveEnemyCount*currentWave;
    }

    void NextWave(){
        Debug.Log("Next Wave!");
    }
}
