using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public InputManager inputManager;

    [HideInInspector] public PlayerParry playerParry;

    [HideInInspector] public Rigidbody2D rb;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        inputManager = GetComponent<InputManager>();

        rb = GetComponent<Rigidbody2D>();
        playerParry = GetComponent<PlayerParry>();
    }
     void OnCollisionEnter2D(Collision2D other){
         playerParry.AttemptParry(other.gameObject);
    }
}


