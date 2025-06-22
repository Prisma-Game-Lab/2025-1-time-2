using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    
    private Quaternion desiredRotation;
    private bool shouldRotate;

    private void Start()
    {
        shouldRotate = false;
        desiredRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        ApplyRotation();
    }

    public void SetDesiredRotation(Vector2 rotationVector) 
    {
        if (rotationVector == Vector2.zero) 
        {
            shouldRotate = false;
            return;
        }

        desiredRotation = Quaternion.LookRotation(Vector3.forward, rotationVector);
        shouldRotate = true;
    }

    private void ApplyRotation() 
    {
        if (!shouldRotate) return;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
    }
}
