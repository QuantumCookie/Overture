using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponMode
{
    PRIMARY, SECONDARY
}

public class Weapon_Master : MonoBehaviour
{
    public WeaponObject weaponObject;
    public bool isReloading;
    public delegate void GeneralEvent();

    public event GeneralEvent OnReloadStart;
    public event GeneralEvent OnReloadComplete;

    public void CallReloadStartEvent()
    {
        isReloading = true;

        if (OnReloadStart != null)
        {
            OnReloadStart();
        }
    }

    public void CallReloadCompleteEvent()
    {
        isReloading = false;

        if (OnReloadComplete != null)
        {
            OnReloadComplete();
        }
    }
}
