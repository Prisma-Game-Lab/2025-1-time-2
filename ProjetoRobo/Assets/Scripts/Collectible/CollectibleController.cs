using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CollectibleController : MonoBehaviour
{
    private Transform tr;
    [SerializeField] private float rotatingSpeed;

    [SerializeField] private int despawnTime;

    private UnityEvent onCollect;

    private float timer;


    // Start is called before the first frame update
    void Start()
    {
        timer = despawnTime;
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
        tr.Rotate(0.0f, 0.0f, rotatingSpeed, Space.Self);
    }

   
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        AudioManager.Instance.PlaySFX("powerup_sfx");
        Debug.Log("Collected!");
        Destroy(gameObject);
    }

}


