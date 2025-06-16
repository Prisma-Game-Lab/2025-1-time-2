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
        uiManager.UpdateHealthUI(currentHealth);
    }

    public void UpdateAmmoUI(int currentAmmo)
    {
        uiManager.UpdateAmmoUI(currentAmmo);
    }
}
