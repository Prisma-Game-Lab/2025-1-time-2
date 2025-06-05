using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("Input Events")]
    [SerializeField] private UnityEvent<Vector2> OnMovement;
    [SerializeField] private UnityEvent<Vector2> OnAim;
    [SerializeField] public UnityEvent OnParry;
    [SerializeField] private UnityEvent OnShoot;

    [Header("Input Variables")]
    [SerializeField] private bool P1Movement = true;
    [Range(0f, 1f)]
    [SerializeField] private float stickDeadzone;
    [Range(0f, 1f)]
    [SerializeField] private float stickUpperzone;

    private Vector2 p1Axis;
    private Vector2 p2Axis;

    public void SwitchPlayerInput()
    {
        print("Changed Input");
        P1Movement = !P1Movement;

        OnAxis(p1Axis, P1Movement);
        OnAxis(p2Axis, !P1Movement);
    }

    public void OnP1Axis(InputAction.CallbackContext inputValue) 
    {
        p1Axis = inputValue.ReadValue<Vector2>();

        OnAxis(p1Axis, P1Movement);
    }

    public void OnP2Axis(InputAction.CallbackContext inputValue)
    {
        p2Axis = inputValue.ReadValue<Vector2>();

        OnAxis(p2Axis, !P1Movement);
    }

    private void OnAxis(Vector2 axis, bool playerMoves) 
    {
        if (axis.magnitude < stickDeadzone)
        {
            //Too little of an input
            axis = Vector2.zero;
        }
        else if (axis.magnitude > stickUpperzone) 
        {
            //Max magnitude at 1
            axis.Normalize();
        }

        if (playerMoves)
        {
            OnMovement?.Invoke(axis);
        }
        else
        {
            OnAim?.Invoke(axis);
        }
    }

    public void OnP1Button(InputAction.CallbackContext inputValue)
    {
        if (inputValue.performed) 
        {
            if (P1Movement)
            {
                OnShoot?.Invoke();
            }
            else
            {
                OnParry?.Invoke();
            }
        }
    }

    public void OnP2Button(InputAction.CallbackContext inputValue)
    {
        if (inputValue.performed)
        {
            if (!P1Movement)
            {
                OnShoot?.Invoke();
            }
            else
            {
                OnParry?.Invoke();
            }
        }
    }
}
