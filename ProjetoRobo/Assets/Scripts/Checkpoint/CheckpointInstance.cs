using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckpointInstance : MonoBehaviour
{
    [Header("Variables")]
    public Transform playerRespawnPos;
    
    [Header("Events")]
    public UnityEvent onRespawn;

    private CheckpointController checkpointControllerRef;
    private int checkpointIndex;

    public void Initialization(CheckpointController checkpointController, int index) 
    {
        checkpointControllerRef = checkpointController;
        checkpointIndex = index;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        checkpointControllerRef.UpdateCurrentCheckpoint(checkpointIndex);

        gameObject.SetActive(false);
    }
}
