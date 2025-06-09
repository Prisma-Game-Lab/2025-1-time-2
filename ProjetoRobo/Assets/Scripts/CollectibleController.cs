using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    private Transform tr;
    [SerializeField]private float rotatingSpeed;

    public int type;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tr.Rotate(rotatingSpeed, 0.0f, rotatingSpeed, Space.Self);
    }

    void Heal()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            if (type == 1)
            {
                EntityHealthController ehc = other.GetComponent<EntityHealthController>();
                ehc.currentHealth += 1;
            }
            if (type == 2)
            {
                PlayerFiring pf = other.GetComponent<PlayerFiring>();
                pf.ammoCount += 1;
            }
            Debug.Log("Collected!");
            Destroy(gameObject);
        }
    }
}
