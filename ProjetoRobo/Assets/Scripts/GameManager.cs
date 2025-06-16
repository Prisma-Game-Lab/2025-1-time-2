using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject laserPoolPrefab;
    private LaserPoolController laserPoolReference;
    public int mortarShotConfiguration = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public LaserPoolController GetLaserPoolController() 
    {
        if (laserPoolReference == null) 
        {
            GameObject laserPool = Instantiate(laserPoolPrefab, Vector3.zero, Quaternion.identity);
            laserPoolReference = laserPool.GetComponent<LaserPoolController>();
        }
        return laserPoolReference;
    }
}
