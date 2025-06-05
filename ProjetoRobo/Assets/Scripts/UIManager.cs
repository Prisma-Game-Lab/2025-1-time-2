using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
   public  TMP_Text vidas;
    public  TMP_Text ammo;



    public GameObject player;

   

    // Update is called once per frame
    void Update()
    {
        //ammo.text = pf.GetComponent<>().shopItems
        //vidas.text = phc.currentHealth.ToString();

        ammo.text = "Balas: " + player.GetComponent<PlayerFiring>().ammoCount.ToString();
        vidas.text = "Vidas: " +  player.GetComponent<PlayerHealthController>().currentHealth.ToString();
        
    }
}
