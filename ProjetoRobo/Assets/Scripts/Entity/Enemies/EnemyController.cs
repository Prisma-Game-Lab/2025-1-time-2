using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool foundPlayer = false;
    private Rigidbody2D rb;
    private Rigidbody2D player;
    [SerializeField]private float speed = 3f;
    [SerializeField]private float maxSpeed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate(){
        if (foundPlayer && player != null){
            Vector2 direction = (player.position - rb.position).normalized;
            if (rb.velocity.magnitude < maxSpeed){
                rb.AddForce(direction * speed, ForceMode2D.Force);
            }

            if (rb.velocity.magnitude > maxSpeed){
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && foundPlayer==false)
        {
            // Handle player trigger
            Debug.Log("Player triggered enemy!");
            foundPlayer = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.CompareTag("Player"))
        {
            // Handle player trigger
            Debug.Log("Player collided enemy!");
        }
    }
}
