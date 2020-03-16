using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Data : MonoBehaviour
{
    public int weaponId;
    public string weaponName;
    public AmmoType ammoType;

    public Texture2D cursor;
    public Texture2D icon;

    public float reloadTime;

    public float damagePrimary;
    public float fireRatePrimary;
    public int fireCostPrimary;

    public bool hasSecondaryFire;

    public float damageSecondary;
    public float fireRateSecondary;
    public int fireCostSecondary;

    /*[SerializeField] private int weaponId { get { return weaponId; } }
    [SerializeField] private string weaponName { get { return weaponName; } }

    [SerializeField] private Texture2D cursor { get { return cursor; } }
    [SerializeField] private Texture2D icon { get { return icon; } }

    [SerializeField] private float damagePrimary { get { return damagePrimary; } }
    [SerializeField] private float fireRatePrimary { get { return fireRatePrimary; } }
    [SerializeField] private float reloadTimePrimary { get { return reloadTimePrimary; } }
    [SerializeField] private int fireCostPrimary { get { return fireCostPrimary; } }

    [SerializeField] private float damageSecondary { get { return damageSecondary; } }
    [SerializeField] private float fireRateSecondary { get { return fireRateSecondary; } }
    [SerializeField] private float reloadTimeSecondary { get { return reloadTimeSecondary; } }
    [SerializeField] private int fireCostSecondary { get { return fireCostSecondary; } }*/
}
