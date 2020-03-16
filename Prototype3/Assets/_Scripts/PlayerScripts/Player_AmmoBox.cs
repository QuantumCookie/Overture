using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ammo
{
    public AmmoType ammoType;
    public int ammoInClip;
    public int clipSize;
    public int ammoInBag;

    public Ammo(AmmoType aAmmoType, int aClipSize, int aAmmoInClip, int aAmmoInBag)
    {
        ammoType = aAmmoType;
        ammoInClip = aAmmoInClip;
        clipSize = aClipSize;
        ammoInBag = aAmmoInBag;
    }
}

public class Player_AmmoBox : MonoBehaviour
{
    private Player_Master playerMaster;

    public Ammo[] ammoBox;

    private Dictionary<AmmoType, Ammo> map;

    private void Awake()
    {
        EarlyInitialize();
    }

    private void OnEnable()
    {
        Debug.Log("Ammo Box enabled");
        playerMaster.OnAmmoPickup += PickedUpAmmo;
    }

    private void EarlyInitialize()
    {
        playerMaster = GetComponent<Player_Master>();

        map = new Dictionary<AmmoType, Ammo>();

        for (int i = 0; i < ammoBox.Length; i++)
        {
            map[ammoBox[i].ammoType] = ammoBox[i];
        }
    }

    private void PickedUpAmmo(AmmoType ammoName,int quantity)
    {
        if (!map.ContainsKey(ammoName)) return;

        Ammo a = map[ammoName];

        a.ammoInClip = Mathf.Min(a.ammoInClip + quantity, a.ammoInBag);
        playerMaster.CallAmmoUpdateEvent();
    }

    public void DeductAmmo(AmmoType ammoName, int quantity)
    {
        if (!map.ContainsKey(ammoName)) return;

        Ammo a = map[ammoName];

        a.ammoInClip -= quantity;
        playerMaster.CallAmmoUpdateEvent();
    }

    public bool HasSufficientAmmo(AmmoType ammoName, int fireCost)
    {
        if (!map.ContainsKey(ammoName)) return false;

        Ammo a = map[ammoName];
        return fireCost <= a.ammoInClip;
    }

    public Ammo GetAmmoData(AmmoType type)
    {
        Debug.Log("Acquiring " + type);
        if (map == null) return null;
        if (map.ContainsKey(type)) return map[type];

        Debug.Log("Failure");

        return null;
    }

    private void OnDisable()
    {
        playerMaster.OnAmmoPickup -= PickedUpAmmo;
    }
}
