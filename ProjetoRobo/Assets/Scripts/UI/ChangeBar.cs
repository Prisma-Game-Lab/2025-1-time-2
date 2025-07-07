using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBar : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Image fillImage;
    [SerializeField] private Image slowFillImage;

    [Header("Variables")]
    [SerializeField] private float slowFillAcceleration;

    private bool slowFillActivated = false;
    private float slowFillVelocity;
    private float lastValue = 1;

    private void FixedUpdate()
    {
        if (slowFillActivated) UpdateSlowFill();
    }

    public void SetCompletion(float percentage) 
    {
        if (lastValue < percentage) DeactivateSlowFill();

        lastValue = fillImage.fillAmount;
        fillImage.fillAmount = percentage;
    }

    public void ActivateSlowFill() 
    {
        if (slowFillActivated) return;

        slowFillImage.gameObject.SetActive(true);
        slowFillImage.fillAmount = lastValue;
        slowFillActivated = true;
        slowFillVelocity = 0;
    }

    public void DeactivateSlowFill()
    {
        if (!slowFillActivated) return;

        slowFillActivated = false;
        slowFillImage.gameObject.SetActive(false);
    }

    private void UpdateSlowFill() 
    {
        slowFillVelocity += slowFillAcceleration * Time.deltaTime;

        slowFillImage.fillAmount -= slowFillVelocity;

        if (slowFillImage.fillAmount <= fillImage.fillAmount)
        {
            DeactivateSlowFill();
            return;
        }
    }
}
