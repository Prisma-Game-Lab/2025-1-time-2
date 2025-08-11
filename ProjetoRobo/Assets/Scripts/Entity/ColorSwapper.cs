using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwapper : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] affectedSpriteRenderers;
    [SerializeField] private Color defaultColor;
    [SerializeField] private float colorSwapDuration;
    private Color[] originalColorsArray;

    private void Start()
    {
        originalColorsArray = new Color[affectedSpriteRenderers.Length];

        for (int i = 0; i < affectedSpriteRenderers.Length; i++)
        {
            SpriteRenderer currentSpriteRenderer = affectedSpriteRenderers[i];
            originalColorsArray[i] = currentSpriteRenderer.color;
        }
    }

    public void SwapColors() 
    {
        foreach (var currentSpriteRenderer in affectedSpriteRenderers)
        {
            currentSpriteRenderer.color = defaultColor;
        }
        StartCoroutine(ChangeBackColor());
    }

    private IEnumerator ChangeBackColor()
    {
        yield return new WaitForSeconds(colorSwapDuration);
        for (int i = 0; i < affectedSpriteRenderers.Length; i++)
        {
            SpriteRenderer currentSpriteRenderer = affectedSpriteRenderers[i];
            currentSpriteRenderer.color = originalColorsArray[i];
        }
    }
}
