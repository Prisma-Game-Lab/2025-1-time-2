using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    
    public UnityEvent OnTrigger;
    
    [Header("Object that Triggers the Event")]
    [SerializeField] private GameObject TriggerObject;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject == TriggerObject)
        {
            OnTrigger.Invoke();
        }
    }
}
