using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWall : MonoBehaviour
{
    [Header("Number of Enemies that Need to Die")]
    [SerializeField] public int enemiesNeeded;


   
    void Update()
    {
        
        if (enemiesNeeded <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void decreaseCounter()
    {
        enemiesNeeded--;
    }
        
    
}
