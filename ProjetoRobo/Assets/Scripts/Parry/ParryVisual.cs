using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryVisual : MonoBehaviour
{

    public Sprite attemptSprite;
    public Sprite successSprite;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
       
    }
    public void ShowSuccessVisual()
    {
        if (spriteRenderer && successSprite)
        {
            spriteRenderer.sprite = successSprite;
        }
    }

    public void ShowAttemptVisual()
    {
       if (spriteRenderer && attemptSprite)
        {
            spriteRenderer.sprite = attemptSprite;
        }  
    }
    
    
}
