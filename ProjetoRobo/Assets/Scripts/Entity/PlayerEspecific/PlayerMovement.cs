using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

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
    [SerializeField] private float dashHealthWindow;
    [SerializeField] private OnTriggerEvent dashHitboxScript;

    private Vector2 moveInput;
    private Vector2 lastStrongMoveInput;

    private float currentAcceleration;
    private float currentDesacceleration;
    private float timeAfterDash;

    public bool dashing { get; private set; } = false ;
    private bool shouldMove = true;
    private bool canDash = true;
    private bool endingDash = false;
    private bool hitDuringDash = false;
    private int closeCallsOnDash;

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
        {
            
            ApplyDashAcceleration();
        }
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

        AudioManager.Instance.PlaySFX("dodge_start_sfx");
        dashing = true;

        shouldMove = false;
        canDash = false;

        hitDuringDash = false;
        closeCallsOnDash = dashHitboxScript.nContacts;
        pc.healthController.OnDamage.AddListener(OnHitDuringDash);

        Vector2 dashDir = pc.playerFiring.aimObject.transform.position - transform.position;
        dashDir.Normalize();
        if (Mathf.Abs(dashDir.magnitude) < 0.5) 
        {
            print(dashDir);
            dashDir = Vector2.right;
        }

        pc.rb.velocity = dashDir * dashSpeed;

        StartCoroutine(EndDash(dashDir.normalized));
        StartCoroutine(EndSuccessfulDash());
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

    private void OnHitDuringDash(int damage)
    {
        hitDuringDash = true;
        pc.healthController.OnDamage.RemoveListener(OnHitDuringDash);
    }

    private IEnumerator EndSuccessfulDash() 
    {
        yield return new WaitForSeconds(dashHealthWindow);

        dashing = false;

        if (!hitDuringDash)
        {
            OnSucessfulDash();
            pc.healthController.OnDamage.RemoveListener(OnHitDuringDash);
        }
    }

    public void AddCloseCalls() 
    {
        if (!dashing) return;

        closeCallsOnDash++;
    }

    private void OnSucessfulDash() 
    {
        if (closeCallsOnDash > 0) 
        {
            AudioManager.Instance.PlaySFX("dodge_sucess_sfx");
            pc.healthController.Heal(1);
        }
    }
}
