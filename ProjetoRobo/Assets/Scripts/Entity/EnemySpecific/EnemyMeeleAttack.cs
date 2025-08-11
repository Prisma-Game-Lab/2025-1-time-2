using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeeleAttack : MonoBehaviour
{
    [SerializeField] private GameObject attackGameObject;
    [SerializeField] private float timeBeforeAttack;
    [SerializeField] private int attackDamage;
    [SerializeField] private LayerMask affectedLayers;
    [SerializeField] private HitboxScript attackHitboxScript;
    
    [HideInInspector] public EnemyCont enemyCont;
    private bool attacking;

    private void Start()
    {
        enemyCont = GetComponent<EnemyCont>();
    }

    public void PerformMeeleAttack() 
    {
        if (!attacking) StartCoroutine(MeeleAttackCoroutine());
    }

    private IEnumerator MeeleAttackCoroutine() 
    {
        attacking = true;
        attackHitboxScript.Initialization(attackDamage, affectedLayers);
        enemyCont.enemyMovement.SetCurrentMaxSpeed(0);
        yield return new WaitForSecondsRealtime(timeBeforeAttack);
        attackGameObject.SetActive(true);
    }

    public void EndAttackState() 
    {
        enemyCont.enemyMovement.SetCurrentMaxSpeed();
        attacking = false;
    }
}
