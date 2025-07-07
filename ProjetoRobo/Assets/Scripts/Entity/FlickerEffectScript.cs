using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerEffectScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer affectedSprite;

    [Range(0, 1)]
    [SerializeField] private float transparentValue;
    [SerializeField] private float phaseDuration;

    private float elapsedTime;
    private bool active;
    private bool transparent;

    private void FixedUpdate()
    {
        if (active) UpdateFlicker();
    }

    public void ActivateFlicker()
    {
        active = true;
        elapsedTime = 0;
        SetTransparency(transparentValue);
        transparent = true;
    }

    public void DeactivateFlicker() 
    {
        active = false;
        SetTransparency(1);
        transparent = false;
    }

    private void UpdateFlicker() 
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= phaseDuration) 
        {
            elapsedTime = 0;

            if (transparent) SetTransparency(1);
            else SetTransparency(transparentValue);
        }
    }

    private void SetTransparency(float value) 
    {
        affectedSprite.color = new Color(affectedSprite.color.r, affectedSprite.color.g, affectedSprite.color.b, value);
    }
}
