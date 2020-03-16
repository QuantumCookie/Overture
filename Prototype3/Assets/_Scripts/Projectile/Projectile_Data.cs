using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Data : MonoBehaviour
{
    public string projectileDescription;
    public float damage, speed;
    public string targetTag, sourceTag;

    public LayerMask collidesWith;
}
