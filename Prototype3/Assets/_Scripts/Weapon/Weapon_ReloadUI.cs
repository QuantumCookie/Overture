using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon_ReloadUI : MonoBehaviour
{
    public Image reloadIndicator;
    public Text reloadText;

    private Weapon_Master weaponMaster;
    private Player_AmmoBox ammoBox;
    private Ammo ammoData;

    public float blinkTimePeriod = 0.01f;
    private Color min, max, current;
    private float timeElapsed, lerpFactor;

    private void OnEnable()
    {
        Initialize();
        UpdateUI();
        weaponMaster.OnReloadStart += ReloadStarted;
        weaponMaster.OnReloadComplete += ReloadComplete;
        
    }

    private void Initialize()
    {
        weaponMaster = GetComponent<Weapon_Master>();
        ammoBox = transform.root.GetComponent<Player_AmmoBox>();
        StartCoroutine(GetAmmoData());
    }

    private IEnumerator GetAmmoData()
    {
        while (ammoData == null)
        {
            ammoData = ammoBox.GetAmmoData(GetComponent<Weapon_Data>().ammoType);
            yield return null;
        }
    }

    private void Update()
    {
        UpdateUI();
    }

    private void ReloadStarted()
    {
        StartCoroutine("ReloadInfoAnimation");
    }

    private void ReloadComplete()
    {
        StopCoroutine("ReloadInfoAnimation");
    }

    private IEnumerator ReloadInfoAnimation()
    {
        min = Color.clear;
        max = Color.white;

        timeElapsed = 0f;

        while(weaponMaster.isReloading)
        {
            lerpFactor = Mathf.Repeat(timeElapsed, 0.5f);

            reloadText.color = Color.Lerp(max, min, lerpFactor);

            timeElapsed += Time.deltaTime * blinkTimePeriod;

            yield return null;
        }

        reloadText.color = Color.white;
    }

    private void UpdateUI()
    {
        if (ammoData == null) return;

        if(weaponMaster.isReloading)
        {
            reloadText.text = "Reloading...";
        }
        else
        {
            if((ammoData.ammoInClip / (float)ammoData.clipSize) < 0.05f)
            {
                reloadText.text = "Press 'R' to reload";
                reloadText.color = Color.white;
            }
            else
            {
                reloadText.text = "";
            }
        }
    }

    private void OnDisable()
    {
        weaponMaster.OnReloadStart -= ReloadStarted;
        weaponMaster.OnReloadComplete -= ReloadComplete;
    }
}
