using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Master : MonoBehaviour
{
    public delegate void GeneralEvent();
    public event GeneralEvent OnAmmoUpdate;

    public delegate void WeaponToggleEvent(int id);
    public event WeaponToggleEvent OnWeaponToggle;

    public delegate void AmmoPickupEvent(AmmoType ammoType, int ammoQuantity);
    public event AmmoPickupEvent OnAmmoPickup;

    public delegate void HealthChangeEvent(float delta);
    public event HealthChangeEvent OnHealthGain;
    public event HealthChangeEvent OnHealthDeduct;

    public void CallWeaponToggleEvent(int id)
    {
        if(OnWeaponToggle != null)
        {
            OnWeaponToggle(id);
        }
    }

    public void CallAmmoPickup(AmmoType ammoType, int ammoQuantity)
    {
        if (OnAmmoPickup != null)
        {
            OnAmmoPickup(ammoType, ammoQuantity);
        }
    }

    public void CallHealthGainEvent(float delta)
    {
        if (OnHealthGain != null)
        {
            OnHealthGain(delta);
        }
    }

    public void CallHealthDeductEvent(float delta)
    {
        if (OnHealthDeduct != null)
        {
            OnHealthDeduct(delta);
        }
    }

    public void CallAmmoUpdateEvent()
    {
        if (OnAmmoUpdate != null)
        {
            OnAmmoUpdate();
        }
    }
}
