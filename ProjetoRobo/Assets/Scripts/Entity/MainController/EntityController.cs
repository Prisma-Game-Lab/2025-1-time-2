using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    [HideInInspector] public DamageController damageController;
    [HideInInspector] public EntityHealthController healthController;

    [HideInInspector] public Rigidbody2D rb;

    protected virtual void Awake()
    {
        damageController = GetComponent<DamageController>();
        healthController = GetComponent<EntityHealthController>();

        rb = GetComponent<Rigidbody2D>();
    }
}
