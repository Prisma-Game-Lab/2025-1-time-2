using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryHitbox : MonoBehaviour
{
    private PlayerParry owner;
       public void Init(PlayerParry parryOwner)
    {
        owner = parryOwner;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        
        if (owner != null)
        {
            owner.AttemptParry(other.gameObject);
        }
    }
}
