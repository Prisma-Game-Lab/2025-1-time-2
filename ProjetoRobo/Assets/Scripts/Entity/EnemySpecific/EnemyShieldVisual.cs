using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShieldVisual : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 5;
       
    }

    public void MeleeShield()
    {
        if (spriteRenderer)
        {
            
            spriteRenderer.color = Color.magenta;
        }
    }

    public void ShotShield()
    {
        if (spriteRenderer)
        {
            spriteRenderer.color = Color.blue;
        }
         
    }
}
