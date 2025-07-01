using System.Collections.Generic;
using TMPro;
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

    [Header("Corners")]

    public GameObject greenMS;
    public GameObject redMS;
    public GameObject greenPA;
    public GameObject redPA;

    [Header("Bars")]

    public Image RedBar;

    public Image GreenBar;


    [Header("Pontos")]
    public TMP_Text pontosText;

    public int pontos = 0;

    private bool b1 = true;
    private bool b2 = false;

    private List<Image> heartIcons = new List<Image>();
    private List<Image> bulletIcons = new List<Image>();

    private int maxHearts;
    private int maxBullets;

    private float maxTime;

    private float currentTime;

    void Start()
    {
        pontosText.text = "0";
        maxHearts = player.GetComponent<PlayerHealthController>().maxHealth / 2;
        maxBullets = player.GetComponent<PlayerFiring>().maxAmmo;

        maxTime = player.GetComponent<PlayerTimer>().maxFixedTimer + player.GetComponent<PlayerTimer>().maxVariableTimer;

        InitIcons(heartContainer, heartPrefab, maxHearts, heartIcons);
        InitIcons(bulletContainer, bulletPrefab, maxBullets, bulletIcons);

        greenPA.SetActive(b1);
        redMS.SetActive(b1);

        greenMS.SetActive(b2);
        redPA.SetActive(b2);
    }

    void Update()
    {
        currentTime = player.GetComponent<PlayerTimer>().currentVariableTimer + player.GetComponent<PlayerTimer>().currentFixedTimer;
        pontosText.text = pontos.ToString();
        FillBar();
        //int currentHealth = player.GetComponent<PlayerHealthController>().currentHealth;
        //int currentAmmo = player.GetComponent<PlayerFiring>().ammoCount;

        //UpdateIconSprites(heartIcons, currentHealth, fullHeartSprite, emptyHeartSprite);
        //UpdateIconSprites(bulletIcons, currentAmmo, fullBulletSprite, emptyBulletSprite);
    }
    private void FillBar()
    {
        GreenBar.fillAmount = currentTime / maxTime;
        RedBar.fillAmount = currentTime / maxTime;
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
       UpdateHeartSprites(heartIcons,currentHealth) ;
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

    public void SwitchCorners()
    {
        b1 = !b1;
        b2 = !b2;

        greenPA.SetActive(b1);
        redMS.SetActive(b1);

        greenMS.SetActive(b2);
        redPA.SetActive(b2);
    }
}

