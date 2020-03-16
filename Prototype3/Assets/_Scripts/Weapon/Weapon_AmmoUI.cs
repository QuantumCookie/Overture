using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon_AmmoUI : MonoBehaviour
{
    public Text ammoInClipText, ammoInBagText;

    private Player_AmmoBox ammoBox;
    private Ammo ammoData;

    private void OnEnable()
    {
        Initialize();
    }

    private void Start()
    {
        LateInitialize();
    }

    private void Initialize()
    {
        ammoBox = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_AmmoBox>();
    }

    private void LateInitialize()
    {
        ammoData = ammoBox.GetAmmoData(GetComponent<Weapon_Data>().ammoType);
    }

    private void LateUpdate()
    {
        ammoInClipText.text = ammoData.ammoInClip.ToString();
        ammoInBagText.text = ammoData.ammoInBag.ToString();
    }

    private void OnDisable()
    {
        
    }
}
