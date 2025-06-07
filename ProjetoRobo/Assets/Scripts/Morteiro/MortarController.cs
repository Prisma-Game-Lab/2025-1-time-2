using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarController : MonoBehaviour
{
    [SerializeField] private AnimationCurve progressCurve;
    [SerializeField] private AnimationCurve scaleCurve;
    [SerializeField] private float scaleModifier;
    [SerializeField] private float hitboxRadius;

    private int mortarDamage;
    private float timeElapsed;
    private float maxDuration;
    private Vector2 starterPosition;
    private Vector2 targetPosition;
    private Vector2 starterScale;
    private LayerMask targetMask;

    public void Initialization(Vector2 target, int damage, float duration, LayerMask hitMask) 
    {
        targetPosition = target;

        starterPosition = transform.position;
        starterScale = transform.localScale;

        mortarDamage = damage;
        maxDuration = duration;
        targetMask = hitMask;
    }

    private void FixedUpdate()
    {
        timeElapsed += Time.deltaTime;
        UpdatePosition();
    }

    private void UpdatePosition() 
    {
        float progress = timeElapsed / maxDuration;
        float easing = progressCurve.Evaluate(progress);
        transform.localScale = starterScale + Vector2.one * scaleModifier * scaleCurve.Evaluate(progress);
        transform.position = Vector2.Lerp(starterPosition, targetPosition, easing);

        if (progress >= 1) 
        {
            HitGround();
            Destroy(gameObject);
        }
    }

    private void HitGround() 
    {
        RaycastHit2D[] hitInformation = Physics2D.CircleCastAll(transform.position, hitboxRadius, Vector2.zero, 0, targetMask);
        foreach (RaycastHit2D hit in hitInformation) 
        {
            //For every hit
            //Do something
            hit.transform.gameObject.GetComponent<DamageController>().OnDamage(mortarDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, hitboxRadius);
    }
}
