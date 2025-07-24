using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SecondaryCooldown : MonoBehaviour
{
    [Header("Cooldown UI")]
    [SerializeField] private Image Backdrop;

    public float currentCooldown = 0f;
    public float currentTime = 0f;
    private bool isOnCooldown = false;

    private void Update()
    {
        if (isOnCooldown)
        {
            currentTime -= Time.deltaTime;
            Backdrop.fillAmount = currentTime / currentCooldown;

            if (currentTime <= 0f)
            {
                currentTime = 0f;
                Backdrop.fillAmount = 1f;
                isOnCooldown = false;
            }
        }
    }

    public void TriggerCooldown(float cooldown)
    {
        currentCooldown = cooldown;
        currentTime = cooldown;
        isOnCooldown = true;
        Backdrop.fillAmount = 1f;
    }
    public bool IsOnCooldown() => isOnCooldown;

    public void SetCooldown(float newCooldown)
    {
        currentCooldown = newCooldown;
    }
    public void SetCurrentTime(float newCurrent)
    {
        currentTime = newCurrent;
    }
    public void SyncVisual()
    {
        if (currentCooldown > 0f && currentTime > 0f)
        {
            isOnCooldown = true;
            Backdrop.fillAmount = currentTime / currentCooldown;
        }
        else
        {
            isOnCooldown = false;
            Backdrop.fillAmount = 1f;
        }
    }
    
    public void ResetCooldown()
    {
    isOnCooldown = false;
    currentTime = 0f;
    Backdrop.fillAmount = 1f;
    }
}

