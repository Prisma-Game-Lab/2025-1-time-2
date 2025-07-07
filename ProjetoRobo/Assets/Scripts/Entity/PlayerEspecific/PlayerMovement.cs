using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Reference to controller
    private PlayerController pc;

    [Header("Basic Variables")]
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private float accelerationRate;
    [SerializeField] private float desaccelerationRate;
    [SerializeField] private float inputMinForRotation;

    [Header("Dash Variables")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashAcceleration;
    [SerializeField] private float dashDesacceleration;
    [SerializeField] private float dashDesaccelerationDuration;
    [SerializeField] private float dashEndForce;

    private Vector2 moveInput;
    private Vector2 lastStrongMoveInput;

    private float currentAcceleration;
    private float currentDesacceleration;
    private float timeAfterDash;
    
    private bool shouldMove = true;
    private bool canDash = true;
    private bool endingDash = false;


    private void Start()
    {
        pc = GetComponent<PlayerController>();

        currentAcceleration = accelerationRate;
        currentDesacceleration = desaccelerationRate;
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        if (endingDash)
            ApplyDashAcceleration();
    }

    public void OnDirectionChange(Vector2 newInputVector)
    {
        moveInput = newInputVector;

        if (moveInput.magnitude > inputMinForRotation) 
        {
            pc.playerTracksRotation?.SetDesiredRotation(moveInput);
            lastStrongMoveInput = moveInput;
        }
        else 
        {
            pc.playerTracksRotation?.SetDesiredRotation(Vector2.zero);
        }
    }

    private void ApplyMovement()
    {
        if (!shouldMove) return;

        Vector2 targetSpeed = moveInput * maxMoveSpeed;

        Vector2 speedDif = targetSpeed - pc.rb.velocity;

        float accelRate;
        if (targetSpeed.magnitude > 0.01f)
        {
            accelRate = currentAcceleration;
        }
        else
        {
            accelRate = currentDesacceleration;
        }

        pc.rb.AddForce(speedDif * accelRate);
    }

    private void ApplyDashAcceleration() 
    {
        timeAfterDash += Time.deltaTime;
        float completionRatio = timeAfterDash / dashDesaccelerationDuration;

        if (completionRatio >= 1) 
        {
            currentAcceleration = accelerationRate;
            currentDesacceleration = desaccelerationRate;
            endingDash = false;
            return;
        }

        currentAcceleration = Mathf.SmoothStep(dashAcceleration, accelerationRate, completionRatio);
        currentDesacceleration = Mathf.SmoothStep(dashDesacceleration, desaccelerationRate, completionRatio);

    }

    public void PerformDash() 
    {
        if (!shouldMove || !canDash) return;

        shouldMove = false;
        canDash = false;

        pc.rb.velocity = lastStrongMoveInput.normalized * dashSpeed;

        StartCoroutine(EndDash(lastStrongMoveInput.normalized));
    }

    private IEnumerator EndDash(Vector2 dashDir) 
    {
        yield return new WaitForSeconds(dashDuration);
        currentAcceleration = dashAcceleration;
        currentDesacceleration = dashDesacceleration;
        pc.rb.AddForce(dashDir * dashSpeed * dashEndForce * 10);
        timeAfterDash = 0;
        endingDash = true;
        shouldMove = true;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
