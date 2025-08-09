using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarController : MonoBehaviour
{
    [SerializeField] private AnimationCurve progressCurve;
    [SerializeField] private AnimationCurve scaleCurve;
    [SerializeField] private float scaleModifier;
    [SerializeField] private float hitboxRadius;
    [SerializeField] private GameObject onHitExplosion;
    [SerializeField] private float destructionTime;

    private int mortarDamage;
    private float timeElapsed;
    private float maxDuration;
    private Vector2 starterPosition;
    private Vector2 targetPosition;
    private Vector2 starterScale;
    private LayerMask targetMask;
    private bool exploding = false;

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
        if (exploding) return;

        float progress = timeElapsed / maxDuration;
        float easing = progressCurve.Evaluate(progress);
        transform.localScale = starterScale + Vector2.one * scaleModifier * scaleCurve.Evaluate(progress);
        transform.position = Vector2.Lerp(starterPosition, targetPosition, easing);

        if (progress >= 1) 
        {
            exploding = true;
            //HitGround();
            DestructionCoroutine();
        }
    }

    //private void HitGround() 
    //{
    //    RaycastHit2D[] hitInformation = Physics2D.CircleCastAll(transform.position, hitboxRadius, Vector2.zero, 0, targetMask);
    //    foreach (RaycastHit2D hit in hitInformation) 
    //    {
    //        //For every hit
    //        //Do something
    //        hit.transform.gameObject.GetComponent<DamageController>().OnDamage(mortarDamage);
    //    }
    //}

    private void DestructionCoroutine() 
    {
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponent<ParticleSystem>().Stop();
        GameObject explosion = Instantiate(onHitExplosion, transform.position, Quaternion.identity);
        explosion.GetComponent<ExplosionScript>().Initialization(mortarDamage, targetMask, hitboxRadius);
        Destroy(gameObject, destructionTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, hitboxRadius);
    }
}
