using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController
{
    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public InputManager inputManager;

    [HideInInspector] public PlayerParry playerParry;

    public bool isAlive = true;
    public bool isInvincible = false;
    public int lives = 5; 

    [HideInInspector] public Rigidbody2D rb;

    protected override void Awake()
    {
        base.Awake();

        playerParry = GetComponent<PlayerParry>();
        playerMovement = GetComponent<PlayerMovement>();
        inputManager = GetComponent<InputManager>();

        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Laser") && isAlive && !isInvincible)
        {
            lives--;
            Debug.Log(lives + " lives left.");
            if(lives <= 0){
                isAlive = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
         playerParry?.AttemptParry(other.gameObject);
    }
}