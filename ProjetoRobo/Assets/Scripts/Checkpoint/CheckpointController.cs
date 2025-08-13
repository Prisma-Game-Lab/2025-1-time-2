using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    [SerializeField]
    private CheckpointInstance[] checkpoints;

    private void Start()
    {
        for (int i = 0; i < checkpoints.Length; i++) 
        {
            CheckpointInstance checkpoint = checkpoints[i];
            checkpoint.Initialization(this, i);
        }

        LevelManager.OnSceneChanged.AddListener(RestartCheckPoint);

        if (GameManager.Instance.currentCheckpointIndex >= 0) LoadCheckpoint();
    }

    public void UpdateCurrentCheckpoint(int checkpointIndex) 
    {
        print(checkpointIndex);
        if (GameManager.Instance.currentCheckpointIndex < checkpointIndex) 
        {
            GameManager.Instance.currentCheckpointIndex = checkpointIndex;
        }
    }

    public void RestartCheckPoint() 
    {
        GameManager.Instance.currentCheckpointIndex = -1;
        LevelManager.OnSceneChanged.RemoveListener(RestartCheckPoint);
    }

    public void LoadCheckpoint() 
    {
        GameObject player = GameManager.Instance.GetPlayerRef();
        checkpoints[GameManager.Instance.currentCheckpointIndex].onRespawn.Invoke();
        player.transform.position = checkpoints[GameManager.Instance.currentCheckpointIndex].playerRespawnPos.position;
    }
}
