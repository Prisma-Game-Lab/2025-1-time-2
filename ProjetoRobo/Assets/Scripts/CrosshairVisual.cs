using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairVisual : MonoBehaviour
{
    public Sprite greenSprite;
    public Sprite redSprite;

    private SpriteRenderer spriteRenderer;

    private bool switched = true;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
       
    }
   
    public void SwitchSprite()
    {
       if (spriteRenderer == null || redSprite == null || greenSprite == null)
            return;

        switched = !switched;
        spriteRenderer.sprite = switched ? redSprite : greenSprite; 
    }
    
}
