using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEvent : MonoBehaviour
{
    [SerializeField] public UnityEvent<int> OnTriggerEnterEvent;
    [SerializeField] public UnityEvent<int> OnTriggerExitedEvent;

    public int nContacts { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        nContacts++;
        print(collision.name);
        OnTriggerEnterEvent.Invoke(nContacts);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        nContacts--;
        OnTriggerExitedEvent.Invoke(nContacts);
    }
}
