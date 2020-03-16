using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Master : MonoBehaviour
{
    public delegate void GeneralEvent();
    public event GeneralEvent OnDestroy;

    public delegate void ProjectileEvent(Transform victim);
    public event ProjectileEvent OnTargetHit;

    public void CallTargetHitEvent(Transform victim)
    {
        if(OnTargetHit != null)
        {
            OnTargetHit(victim);
        }
    }

    public void CallDestroyEvent()
    {
        if (OnDestroy != null)
        {
            OnDestroy();
        }
    }
}
