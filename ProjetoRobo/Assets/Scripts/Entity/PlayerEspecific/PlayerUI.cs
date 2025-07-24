using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    private PlayerController playerController;
    private UIManager uiManager;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public void UpdateHealthUI(int currentHealth) 
    {
        uiManager?.UpdateHealthUI(currentHealth);
    }

    public void UpdateAmmoUI(int currentAmmo)
    {
        uiManager?.UpdateAmmoUI(currentAmmo);
    }

    public void UpdateBars(float percentage, bool activateSlowFill = false) 
    {
        uiManager?.RedBarController.SetCompletion(percentage);
        uiManager?.GreenBarController.SetCompletion(percentage);

        if (activateSlowFill) 
        {
            uiManager?.RedBarController.ActivateSlowFill();
            uiManager?.GreenBarController.ActivateSlowFill();
        }
    }

    public void ChangeIcons(bool P1Movement) 
    {
        uiManager.SwitchCorners(P1Movement);
    }

    public void UpdateDefensiveMorph() 
    {
        uiManager.MorphDefensive();
    }

    public void UpdateOffensiveMorph()
    {
        uiManager.MorphOffensive();
    }
}
