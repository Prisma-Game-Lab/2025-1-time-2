using System.Collections.Generic;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Containers")]
    public Transform heartContainer;
    public Transform bulletContainer;

    [Header("Sprites")]
    public Sprite fullHeartSprite;

    public Sprite halfHeartSprite;
    public Sprite emptyHeartSprite;
    public Sprite fullBulletSprite;
    public Sprite emptyBulletSprite;

    [Header("Prefabs")]
    public GameObject heartPrefab;
    public GameObject bulletPrefab;

    [Header("References")]
    public GameObject player;

    [SerializeField] GameObject UI;


    [SerializeField] private Animator greenIconAnimator;
    [SerializeField] private Animator redIconAnimator;

    [SerializeField] private Animator redSecondaryAnimator;

    [SerializeField] private Animator greenSecondaryAnimator;

    [Header("Corners")]
    [SerializeField] private GameObject greenMove;
    [SerializeField] private GameObject redMove;
    [SerializeField] private GameObject greenAim;
    [SerializeField] private GameObject redAim;

    [Header("Secondary Functions Icons")]

    [SerializeField] private GameObject GparryIcon;

    [SerializeField] private GameObject GshootIcon;

    [SerializeField] private GameObject GmeleeIcon;

    [SerializeField] private GameObject GdodgeIcon;

    [SerializeField] private GameObject RparryIcon;

    [SerializeField] private GameObject RshootIcon;

    [SerializeField] private GameObject RmeleeIcon;

    [SerializeField] private GameObject RdodgeIcon;

    [SerializeField] private SecondaryCooldown greenCD;

    [SerializeField] private SecondaryCooldown redCD;



    private bool isShotActive = true, isParryActive = true;

    private float parryCooldown, shotCooldown, dashCooldown, meleeCooldown;



    [Header("Bars")]
    public ChangeBar RedBarController;
    public ChangeBar GreenBarController;


    [Header("Pontos")]
    public TMP_Text pontosText;

    public int pontos = 0;


    private List<Image> heartIcons = new List<Image>();
    private List<Image> bulletIcons = new List<Image>();

    private int maxHearts;
    private int maxBullets;

    //private float maxTime;



    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        UI.SetActive(true);
        pontosText.text = "0";
        maxHearts = player.GetComponent<PlayerHealthController>().maxHealth / 2;
        maxBullets = player.GetComponent<PlayerFiring>().maxAmmo;

        //maxTime = player.GetComponent<PlayerTimer>().maxFixedTimer + player.GetComponent<PlayerTimer>().maxVariableTimer;

        InitIcons(heartContainer, heartPrefab, maxHearts, heartIcons);
        InitIcons(bulletContainer, bulletPrefab, maxBullets, bulletIcons);

        greenAim.SetActive(true);
        redMove.SetActive(true);

        greenMove.SetActive(false);
        redAim.SetActive(false);

        parryCooldown = player.GetComponent<PlayerParry>().parryCooldown;
        dashCooldown = player.GetComponent<PlayerMovement>().dashCooldown;

        shotCooldown = 0;
        meleeCooldown = player.GetComponent<PlayerFiring>().meleeCooldown;

        greenCD = greenCD.GetComponent<SecondaryCooldown>();
        redCD = redCD.GetComponent<SecondaryCooldown>();


    }

    void Update()
    {

        //currentTime = player.GetComponent<PlayerTimer>().currentVariableTimer + player.GetComponent<PlayerTimer>().currentFixedTimer;
        pontosText.text = pontos.ToString();

        //FillBar();
        //int currentHealth = player.GetComponent<PlayerHealthController>().currentHealth;
        //int currentAmmo = player.GetComponent<PlayerFiring>().ammoCount;

        //UpdateIconSprites(heartIcons, currentHealth, fullHeartSprite, emptyHeartSprite);
        //UpdateIconSprites(bulletIcons, currentAmmo, fullBulletSprite, emptyBulletSprite);
    }




    private void InitIcons(Transform container, GameObject prefab, int count, List<Image> iconList)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject icon = Instantiate(prefab, container);
            Image img = icon.GetComponent<Image>();
            iconList.Add(img);
        }
    }

    public void UpdateHealthUI(int currentHealth)
    {
        UpdateHeartSprites(heartIcons, currentHealth);
    }

    public void UpdateAmmoUI(int currentAmmo)
    {
        UpdateIconSprites(bulletIcons, currentAmmo, fullBulletSprite, emptyBulletSprite);
    }

    private void UpdateIconSprites(List<Image> icons, int activeCount, Sprite fullSprite, Sprite emptySprite)
    {
        for (int i = 0; i < icons.Count; i++)
        {
            icons[i].sprite = i < activeCount ? fullSprite : emptySprite;
        }
    }

    private void UpdateHeartSprites(List<Image> icons, int currentHealth)
    {
        for (int i = 0; i < icons.Count; i++)
        {
            int heartValue = i * 2;

            if (currentHealth >= heartValue + 2)
            {
                icons[i].sprite = fullHeartSprite;
            }
            else if (currentHealth == heartValue + 1)
            {
                icons[i].sprite = halfHeartSprite;
            }
            else
            {
                icons[i].sprite = emptyHeartSprite;
            }
        }
    }

    private IEnumerator DelayedIconSwitch(GameObject icon1, GameObject icon2, bool wasActive)
    {
    yield return new WaitForSeconds(0.3f); 

    icon1.SetActive(!wasActive);  
    icon2.SetActive(wasActive);
    }
    
    public void MorphOffensive()
    {
        bool P1Movement = player.GetComponent<InputManager>().P1Movement;
        float newCooldown = isShotActive ? meleeCooldown : shotCooldown;

        if (P1Movement)
        {
            RshootIcon.SetActive(true);
            RmeleeIcon.SetActive(true);

            if (isShotActive)
                redSecondaryAnimator.Play("ShootToMelee");
            else
                redSecondaryAnimator.Play("MeleeToShoot");

            //RshootIcon.SetActive(!isShotActive);
            //RmeleeIcon.SetActive(isShotActive);

            redCD.SetCooldown(newCooldown);
            redCD.SetCurrentTime(newCooldown);
            redCD.ResetCooldown();

            StartCoroutine(DelayedIconSwitch(RshootIcon, RmeleeIcon, isShotActive));

        }
        else
        {
            GshootIcon.SetActive(true);
            GmeleeIcon.SetActive(true);

            if (isShotActive)
                greenSecondaryAnimator.Play("ShootToMelee");
            else
                greenSecondaryAnimator.Play("MeleeToShoot");

            //GshootIcon.SetActive(!isShotActive);
            //GmeleeIcon.SetActive(isShotActive);

            greenCD.SetCooldown(newCooldown);
            greenCD.SetCurrentTime(newCooldown);
            greenCD.ResetCooldown();

            StartCoroutine(DelayedIconSwitch(GshootIcon, GmeleeIcon, isShotActive));


        }
        AudioManager.Instance.PlaySFX("morph_sfx");
        isShotActive = !isShotActive;
        greenCD.SyncVisual();
        redCD.SyncVisual();
    }
    public void MorphDefensive()
    {
        bool P1Movement = player.GetComponent<InputManager>().P1Movement;
        float newCooldown = isParryActive ? dashCooldown : parryCooldown;

        if (P1Movement)
        {
            GparryIcon.SetActive(true);
            GdodgeIcon.SetActive(true);

            if (isParryActive)
                greenSecondaryAnimator.Play("ParryToDodge");
            else
                greenSecondaryAnimator.Play("DodgeToParry");

            //GparryIcon.SetActive(!isParryActive);
            //GdodgeIcon.SetActive(isParryActive);

            greenCD.SetCooldown(newCooldown);
            greenCD.SetCurrentTime(newCooldown);
            greenCD.ResetCooldown();

            StartCoroutine(DelayedIconSwitch(GparryIcon, GdodgeIcon, isParryActive));
        }
        else
        {

            RparryIcon.SetActive(true);
            RdodgeIcon.SetActive(true);

            if (isParryActive)
                redSecondaryAnimator.Play("ParryToDodge");
            else
                redSecondaryAnimator.Play("DodgeToParry");

            //RparryIcon.SetActive(!isParryActive);
            //RdodgeIcon.SetActive(isParryActive);

            redCD.SetCooldown(newCooldown);
            redCD.SetCurrentTime(newCooldown);
            redCD.ResetCooldown();

            StartCoroutine(DelayedIconSwitch(RparryIcon, RdodgeIcon, isParryActive));
        }
        AudioManager.Instance.PlaySFX("morph_sfx");
        isParryActive = !isParryActive;

        greenCD.SyncVisual();
        redCD.SyncVisual();
    }
    public void TriggerSecondaryCooldown(bool isOffensive)
    {
        bool isP1 = player.GetComponent<InputManager>().P1Movement;

        if (isOffensive)
        {
            if (isShotActive)
            {
                // Currently in shoot mode
                if (isP1)
                    redCD.TriggerCooldown(shotCooldown);
                else
                    greenCD.TriggerCooldown(shotCooldown);
            }
            else
            {
                // Currently in melee mode
                if (isP1)
                    redCD.TriggerCooldown(meleeCooldown);
                else
                    greenCD.TriggerCooldown(meleeCooldown);
            }
        }
        else
        {
            if (isParryActive)
            {
                // Currently in parry mode
                if (isP1)
                    greenCD.TriggerCooldown(parryCooldown);
                else
                    redCD.TriggerCooldown(parryCooldown);
            }
            else
            {
                // Currently in dash mode
                if (isP1)
                    greenCD.TriggerCooldown(dashCooldown);
                else
                    redCD.TriggerCooldown(dashCooldown);
            }
        }
    }
    public void SwitchSecondaryIcons(bool P1Movement)
    {



        GshootIcon.SetActive(false);
        GmeleeIcon.SetActive(false);
        GparryIcon.SetActive(false);
        GdodgeIcon.SetActive(false);

        RshootIcon.SetActive(false);
        RmeleeIcon.SetActive(false);
        RparryIcon.SetActive(false);
        RdodgeIcon.SetActive(false);

        // Offensive Icons
        if (isShotActive)
        {
            if (P1Movement)
                RshootIcon.SetActive(true);
            else
                GshootIcon.SetActive(true);
        }
        else
        {
            if (P1Movement)
                RmeleeIcon.SetActive(true);
            else
                GmeleeIcon.SetActive(true);
        }

        // Defensive Icons
        if (isParryActive)
        {
            if (P1Movement)
                GparryIcon.SetActive(true);
            else
                RparryIcon.SetActive(true);
        }
        else
        {
            if (P1Movement)
                GdodgeIcon.SetActive(true);
            else
                RdodgeIcon.SetActive(true);
        }

    }
    public void SwitchCorners(bool P1Movement)
    {
        //b1 = !b1;
        //b2 = !b2;

        //greenPA.SetActive(b1);
        //redMS.SetActive(b1);

        //greenMS.SetActive(b2);
        //redPA.SetActive(b2);

        float tempCooldown = greenCD.currentCooldown;
        float tempTime = greenCD.currentTime;

        SwitchSecondaryIcons(P1Movement);

        greenCD.SetCooldown(redCD.currentCooldown);
        greenCD.SetCurrentTime(redCD.currentTime);

        redCD.SetCooldown(tempCooldown);
        redCD.SetCurrentTime(tempTime);

        greenCD.SyncVisual();
        redCD.SyncVisual();

        if (P1Movement)
        {



            redIconAnimator.Play("ProjectileToMovement");
            greenIconAnimator.Play("MovementToProjectile");
        }
        else
        {


            redIconAnimator.Play("MovementToProjectile");
            greenIconAnimator.Play("ProjectileToMovement");
        }
    }
    
    
}

