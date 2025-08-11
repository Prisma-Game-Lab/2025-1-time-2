using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCont : EntityController
{
    public EnemyMovement enemyMovement { get; private set; }
    public PlayerBodyRotation enemyRotation;
    public EnemyFiring enemyFiring { get; private set; }
    public EnemyMeeleAttack enemyMeele { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        enemyMovement = GetComponent<EnemyMovement>();
        enemyFiring = GetComponent<EnemyFiring>();
        enemyMeele = GetComponent<EnemyMeeleAttack>();
    }
}
