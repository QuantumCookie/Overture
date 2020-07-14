using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Scriptable Objects/Weapon")]
public class WeaponObject : ScriptableObject
{
    public int id;
    public string name;
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
}