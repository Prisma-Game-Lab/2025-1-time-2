using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvisibleWall : MonoBehaviour
{
    [Header("Number of Enemies that Need to Die")]
    [SerializeField] public int enemiesNeeded;

    public UnityEvent OnDestroy;

   
    void Update()
    {

        if (enemiesNeeded <= 0)
        {
            OnDestroy?.Invoke();
            Destroy(gameObject);
            
        }
    }

    public void decreaseCounter()
    {
        enemiesNeeded--;
    }
        
    
}
