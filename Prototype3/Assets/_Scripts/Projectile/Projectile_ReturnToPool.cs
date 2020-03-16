using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_ReturnToPool : MonoBehaviour
{
    private Projectile_Master projectileMaster;

    private void OnEnable()
    {
        Initialize();
        projectileMaster.OnDestroy += ReturnToPool;
    }

    private void Initialize()
    {
        projectileMaster = GetComponent<Projectile_Master>();
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        projectileMaster.OnDestroy -= ReturnToPool;
    }
}
