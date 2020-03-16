using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Reload : MonoBehaviour
{
    private Weapon_Master weaponMaster;
    private Weapon_Data weaponData;
    private Player_AmmoBox ammoBox;
    private Player_Master playerMaster;

    private Ammo ammoData;

    private bool reloading;
    private float counter, interval;

    private void OnEnable()
    {
        Debug.Log(this.name + " Enabled");
        Initialize();
        playerMaster.OnWeaponToggle += CancelReload;
        weaponMaster.OnReloadStart += Reload;
    }

    private void Start()
    {
        LateInitialize();
    }

    private void Initialize()
    {
        weaponMaster = GetComponent<Weapon_Master>();
        weaponData = GetComponent<Weapon_Data>();
        ammoBox = transform.root.GetComponent<Player_AmmoBox>();
        playerMaster = transform.root.GetComponent<Player_Master>();
    }

    private void LateInitialize()
    {
        ammoData = ammoBox.GetAmmoData(weaponData.ammoType);
    }

    private void Update()
    {
        CheckForReloadRequest();
        AnimateAmmoValue();
    }

    private void AnimateAmmoValue()
    {
        if (!reloading) return;

        counter += Time.deltaTime;

        if(counter > interval)
        {
            ammoData.ammoInClip++;
            ammoData.ammoInBag--;
            counter = 0;

            if(ammoData.ammoInClip == ammoData.clipSize || ammoData.ammoInBag == 0)
            {
                reloading = false;
                weaponMaster.CallReloadCompleteEvent();
            }
        }
    }

    private void CheckForReloadRequest()
    {
        if (Input.GetKeyDown(KeyCode.R) && CanReload())
        {
            Debug.Log("Reload Started");
            weaponMaster.CallReloadStartEvent();
        }
    }

    private bool CanReload()
    {
        if (ammoData.ammoInClip == ammoData.clipSize)
        {
            Debug.Log("Clip Full");
            return false;
        }

        if (ammoData.ammoInBag == 0)
        {
            Debug.Log("Not enough ammo");
            return false;
        }

        if (weaponMaster.isReloading)
        {
            Debug.Log("Already Reloading");
            return false;
        }

        return true;
    }

    private void Reload()
    {
        reloading = true;
        interval = weaponData.reloadTime / ammoData.clipSize;
    }

    private void CancelReload(int id)
    {
        if (!weaponMaster.isReloading) return;

        Debug.Log("Reload Cancelled");

        reloading = false;
        weaponMaster.isReloading = false;
    }

    private void OnDisable()
    {
        playerMaster.OnWeaponToggle -= CancelReload;
        weaponMaster.OnReloadStart -= Reload;
    }
}