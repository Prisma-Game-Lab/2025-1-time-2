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

    public enum CollectibleType { Heal, Ammo }
    [SerializeField] private CollectibleType type;
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
        tr.Rotate(rotatingSpeed, 0.0f, rotatingSpeed, Space.Self);
    }

   
    private void OnTriggerEnter2D(Collider2D other)
    {
    if (!other.CompareTag("Player")) return;

    switch (type)
    {
        case CollectibleType.Heal:
            var ehc = other.GetComponent<EntityHealthController>();
            if (ehc != null && ehc.currentHealth < ehc.maxHealth)
            {
                ehc.Heal(1);
            }
            break;

        case CollectibleType.Ammo:
            var pf = other.GetComponent<PlayerFiring>();
            if (pf != null && pf.ammoCount < pf.maxAmmo)
            {
                pf.ammoCount += 1;
            }
            break;
    }

        AudioManager.Instance.PlaySFX("powerup_sfx");
        Debug.Log("Collected!");
        Destroy(gameObject);
}

    }


