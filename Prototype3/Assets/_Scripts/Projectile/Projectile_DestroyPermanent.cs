using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_DestroyPermanent : MonoBehaviour
{
    private Projectile_Master projectileMaster;

    private void OnEnable()
    {
        Initialize();
        projectileMaster.OnDestroy += DestroyPermanent;
    }

    private void Initialize()
    {
        projectileMaster = GetComponent<Projectile_Master>();
    }

    private void DestroyPermanent()
    {
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        projectileMaster.OnDestroy -= DestroyPermanent;
    }
}
