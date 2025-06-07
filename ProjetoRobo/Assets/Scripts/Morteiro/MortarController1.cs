using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarController1 : MonoBehaviour
{
    [SerializeField] private AnimationCurve progressCurve;
    [SerializeField] private AnimationCurve scaleCurve;
    [SerializeField] private float scaleModifier;
    [SerializeField] private float hitboxRadius;

    private SpriteRenderer spriteRenderer;
    private bool hidden;
    private int mortarDamage;
    private float timeElapsed;
    private float maxDuration;
    private float timeHidden;
    private Vector2 starterScale;
    private LayerMask targetMask;

    public void Initialization(int damage, float duration, float time, LayerMask hitMask) 
    {
        starterScale = transform.localScale * 2;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        hidden = true;

        mortarDamage = damage;
        maxDuration = duration - time;
        targetMask = hitMask;
        timeHidden = time;
    }

    private void FixedUpdate()
    {
        timeElapsed += Time.deltaTime;
        if (hidden) 
        {
            if (timeElapsed > timeHidden) 
            {
                hidden = false;
                spriteRenderer.enabled = true;
                timeElapsed = 0;
            }
        }
        else 
        {
            UpdatePosition();
        }
    }

    private void UpdatePosition() 
    {
        float progress = timeElapsed / maxDuration;
        transform.localScale = starterScale + Vector2.one * scaleModifier * scaleCurve.Evaluate(progress);

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
