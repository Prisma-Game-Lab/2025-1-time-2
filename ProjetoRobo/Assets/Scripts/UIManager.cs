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
    public Sprite emptyHeartSprite;
    public Sprite fullBulletSprite;
    public Sprite emptyBulletSprite;

    [Header("Prefabs")]
    public GameObject heartPrefab;
    public GameObject bulletPrefab;

    [Header("References")]
    public GameObject player;

    [Header("Corners")]

    public GameObject greenL;
    public GameObject redL;
    public GameObject greenR;
    public GameObject redR;

    public TMP_Text pontosText;

    public int pontos = 0;

    private bool b1 = true;
    private bool b2 = false;

    private List<Image> heartIcons = new List<Image>();
    private List<Image> bulletIcons = new List<Image>();

    private int maxHearts;
    private int maxBullets;

    void Start()
    {
        pontosText.text = "0";
        maxHearts = player.GetComponent<PlayerHealthController>().maxHealth;
        maxBullets = player.GetComponent<PlayerFiring>().maxAmmo;

        InitIcons(heartContainer, heartPrefab, maxHearts, heartIcons);
        InitIcons(bulletContainer, bulletPrefab, maxBullets, bulletIcons);

        greenL.SetActive(b1);
        redR.SetActive(b1);

        greenR.SetActive(b2);
        redL.SetActive(b2);
    }

    void Update()
    {
        pontosText.text = pontos.ToString();
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
        UpdateIconSprites(heartIcons, currentHealth, fullHeartSprite, emptyHeartSprite);
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
    public void SwitchCorners()
    {
        b1 = !b1;
        b2 = !b2;

        greenL.SetActive(b1);
        redR.SetActive(b1);

        greenR.SetActive(b2);
        redL.SetActive(b2);
    }
}

