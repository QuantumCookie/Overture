using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_SimpleExplosion : MonoBehaviour
{
    public GameObject explosionFX;

    private Projectile_Master projectileMaster;

    private void OnEnable()
    {
        Initialize();
        projectileMaster.OnTargetHit += Explode;
    }

    private void Initialize()
    {
        projectileMaster = GetComponent<Projectile_Master>();
    }

    private void Explode(Transform other)
    {
        if(explosionFX != null)
        {
            Instantiate(explosionFX, transform.position, transform.rotation, other);
        }

        projectileMaster.CallDestroyEvent();
    }

    private void OnDisable()
    {
        projectileMaster.OnTargetHit -= Explode;
    }
}
