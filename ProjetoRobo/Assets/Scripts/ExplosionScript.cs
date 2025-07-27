using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private CircleCollider2D explosionCollider;
    [SerializeField] private HitboxScript explosionHurtBoxScript;

    [SerializeField] private float destructionTime;

    public void Initialization(int damage, LayerMask affectedLayers, float hitboxSize) 
    {
        float newScale = hitboxSize / explosionCollider.radius;
        transform.localScale = new Vector2(newScale, newScale);
        explosionHurtBoxScript.Initialization(damage, affectedLayers);
    }

    public void OnAnimationEnd() 
    {
        explosionParticles.Stop();
        explosionCollider.enabled = false;
        Destroy(gameObject, destructionTime);
    }
}
