using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarController3 : MonoBehaviour
{
    [SerializeField] private AnimationCurve progressCurve;
    [SerializeField] private AnimationCurve scaleCurve;
    [SerializeField] private float scaleModifier;
    [SerializeField] private float hitboxRadius;
    private Rigidbody2D rb;

    private Vector2 dirVector;
    private Vector2 maxVector;
    private float dirMagnitude;
    private float vectorRatio;
    private float maxSpeed;
    private float currentSpeed;
    private float maxRange;

    private int mortarDamage;
    private float timeElapsed;
    private float maxDuration;
    private Vector2 starterPosition;
    private Vector2 targetPosition;
    private Vector2 starterScale;
    private LayerMask targetMask;

    public void Initialization(Vector2 target, float speed, int damage, float range, LayerMask hitMask) 
    {
        starterPosition = transform.position;

        dirVector = target - (Vector2)transform.position;
        maxVector = dirVector.normalized * range;
        dirMagnitude = dirVector.magnitude;
        vectorRatio = dirVector.magnitude / maxVector.magnitude;
        starterScale = transform.localScale;

        rb = GetComponent<Rigidbody2D>();

        maxSpeed = speed;

        mortarDamage = damage;
        maxRange = range;
        targetMask = hitMask;
        rb.velocity = dirVector.normalized * maxSpeed;
    }

    private void FixedUpdate()
    {
        timeElapsed += Time.deltaTime;
        UpdatePosition();
    }

    private void UpdatePosition() 
    {
        float progress = (starterPosition - (Vector2)transform.position).magnitude / dirMagnitude;
        float easing = progressCurve.Evaluate(progress);
        //rb.velocity = dirVector.normalized * maxSpeed * progressCurve.Evaluate(progress);
        transform.localScale = starterScale + Vector2.one * scaleModifier * scaleCurve.Evaluate(progress) * vectorRatio;

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
