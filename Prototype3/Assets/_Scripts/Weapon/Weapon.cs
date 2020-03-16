using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public string ammoType;
    public Texture2D crosshair;

    public float damagePerScond, damagePerHit;
    public float reloadTime;
}
