using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxScript : MonoBehaviour
{
    private List<GameObject> hitObjects = new List<GameObject>();
    private int HurtBoxdamage;

    public void Initialization(int damage, LayerMask affectedLayers)
    {
        HurtBoxdamage = damage;
        GetComponent<Collider2D>().includeLayers = affectedLayers;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hitObjects.Contains(collision.gameObject)) return;

        hitObjects.Add(collision.gameObject);
        collision.transform.gameObject.GetComponent<DamageController>()?.OnDamage(HurtBoxdamage);
    }
}
