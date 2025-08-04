using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseController : MonoBehaviour
{
    public void OnContinue() 
    {
        LevelManager.LoadSceneByName("EntitiesTest");
    }
}
