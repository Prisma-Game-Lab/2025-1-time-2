using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashEffectScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer affectedSprite;

    [SerializeField] private Material flashMaterial;
    [SerializeField] private float flashDuration;

    private Material originalMaterial;


    public void ActivateFlash() 
    {
        originalMaterial = affectedSprite.material;
        affectedSprite.material = flashMaterial;
        if (gameObject.activeInHierarchy) StartCoroutine(WaitForFlash());
    }

    private IEnumerator WaitForFlash() 
    {
        yield return new WaitForSeconds(flashDuration);
        affectedSprite.material = originalMaterial;
    }
    
}
