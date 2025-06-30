using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerActionController : MonoBehaviour
{
    //Reference to main Controller
    private PlayerController pc;

    [SerializeField] private UnityEvent[] MovementActions;
    [SerializeField] private UnityEvent[] ProjectileActions;

    private int MovementActionIndex = 0;
    private int ProjectileActionIndex = 0;

    private int maxMovementActionIndex;
    private int maxProjectileActionIndex;

    void Start()
    {
        pc = GetComponent<PlayerController>();

        maxMovementActionIndex = MovementActions.Length;
        maxProjectileActionIndex = ProjectileActions.Length;
    }

    public void TriggerCurrentMovementAction() 
    {
        MovementActions[MovementActionIndex].Invoke();
    }

    public void TriggerCurrentProjectileAction()
    {
        ProjectileActions[ProjectileActionIndex].Invoke();
    }

    public void ChangeCurrentMovementAction(int increaseAmount) 
    {
        MovementActionIndex += increaseAmount;
        if (MovementActionIndex >= maxMovementActionIndex) 
        {
            MovementActionIndex -= maxMovementActionIndex;
        }
        else if (MovementActionIndex < 0) 
        {
            MovementActionIndex += maxMovementActionIndex;
        }
    }

    public void ChangeCurrentProjectileAction(int increaseAmount)
    {
        ProjectileActionIndex += increaseAmount;
        if (ProjectileActionIndex >= maxProjectileActionIndex)
        {
            ProjectileActionIndex -= maxProjectileActionIndex;
        }
        else if (ProjectileActionIndex < 0)
        {
            ProjectileActionIndex += maxProjectileActionIndex;
        }
    }
}
