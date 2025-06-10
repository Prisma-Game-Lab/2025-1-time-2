using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Reference to controller
    private PlayerController pc;

    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private float accelerationRate;
    [SerializeField] private float desaccelerationRate;
    
    private Vector2 moveInput;

    private void Start()
    {
        pc = GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    public void OnDirectionChange(Vector2 newInputVector)
    {
        moveInput = newInputVector;
        float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
        pc.rb.rotation = angle;
    }

    private void ApplyMovement()
    {
        Vector2 targetSpeed = moveInput * maxMoveSpeed;

        Vector2 speedDif = targetSpeed - pc.rb.velocity;

        float accelRate;
        if (targetSpeed.magnitude > 0.01f)
        {
            accelRate = accelerationRate;
        }
        else
        {
            accelRate = desaccelerationRate;
        }

        pc.rb.AddForce(speedDif * accelRate);
    }
}
