using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    public DamageController damageController;
    [HideInInspector] public EntityHealthController healthController;

    [HideInInspector] public Rigidbody2D rb;

    protected virtual void Awake()
    {
        DamageController tempdamageController = GetComponent<DamageController>();
        if (tempdamageController != null) damageController = tempdamageController;

        healthController = GetComponent<EntityHealthController>();

        rb = GetComponent<Rigidbody2D>();
    }
}
