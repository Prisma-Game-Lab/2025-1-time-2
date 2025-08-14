using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeeleAttack : MonoBehaviour
{
    [SerializeField] private GameObject attackGameObject;
    [SerializeField] private float timeBeforeAttack;
    [SerializeField] private float attackDuration;
    [SerializeField] private int attackDamage;
    [SerializeField] private LayerMask affectedLayers;
    [SerializeField] private HitboxScript attackHitboxScript;
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private Animator attackAnimator;
    
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
        enemyAnimator.Play("AttackWindUp");
        enemyAnimator.SetFloat("AttackWindUp", 1 / timeBeforeAttack);

        yield return new WaitForSecondsRealtime(timeBeforeAttack);

        enemyAnimator.SetFloat("AttackSpeed", 1 / attackDuration);
        attackGameObject.SetActive(true);
        attackAnimator.SetFloat("AttackSpeed", 1 / attackDuration);

        yield return new WaitForSecondsRealtime(attackDuration);

        EndAttackState();
    }

    public void EndAttackState() 
    {
        enemyCont.enemyMovement.SetCurrentMaxSpeed();
        attackGameObject.SetActive(false);
        attacking = false;
    }
}
