using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerFiring : MonoBehaviour
{
    //Reference to controller
    private PlayerController pc;

    [Header("Aim Configuration")]
    [SerializeField] private GameObject aimPrefab;
    [SerializeField] private float aimMaxSpeed;
    [SerializeField] private float aimAccelerationRate;
    [SerializeField] private float aimDesaccelerationRate;
    [SerializeField] private float aimRadius;

    private GameObject aimObject;
    private Rigidbody2D aimRb;
    private Vector2 aimInput;

    private void Start()
    {
        pc = GetComponent<PlayerController>();

        aimObject = Instantiate(aimPrefab, transform.position, Quaternion.identity);
        aimRb = aimObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveAim();
        RestrictCenter();
    }

    public void OnAimInputChanged(Vector2 newInput) 
    {
        aimInput = newInput;
    }

    private void MoveAim() 
    {
        Vector2 targetSpeed = aimInput * aimMaxSpeed;

        Vector2 speedDif = targetSpeed - aimRb.velocity;

        float accelRate;
        if (targetSpeed.magnitude > 0.01f)
        {
            accelRate = aimAccelerationRate;
        }
        else
        {
            accelRate = aimDesaccelerationRate;
        }

        aimRb.AddForce(speedDif * accelRate);
    }

    private void RestrictCenter() 
    {
        if (Vector2.Distance(transform.position, aimObject.transform.position) > aimRadius) 
        {
            Vector2 directionVector = aimObject.transform.position - transform.position;

            directionVector.Normalize();

            aimObject.transform.position = (Vector2)transform.position + directionVector * aimRadius;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, aimRadius);
    }
}
