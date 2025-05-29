using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    private Transform tr;
    [SerializeField]private float rotatingSpeed;

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

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            Debug.Log("Collected!");
            Destroy(gameObject);
        }
    }
}
