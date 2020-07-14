using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Spektr;

enum RICFireMode
{
    NULL, SOURCE, SINK, DAMAGE
}

public class Weapon_RIC : MonoBehaviour
{
    public Camera mainCamera;
    public Transform ricBarrelTransform;
    public float sphereCastRadius = 2.5f;
    public Transform marker;
    public LayerMask floorMask, ricScanMask;

    private LightningRenderer lightningRenderer;

    [SerializeField] private bool chargeCycleDone, inCoolDown;
    [SerializeField] private float chargeTransferRate = 5f;
    [SerializeField] private Transform currentTarget;
    [SerializeField] private PowerSource currentPowerSrc;
    [SerializeField] private RICFireMode fireMode;
    [SerializeField] [ColorUsage(false, true)] private Color damageMode, sourceMode, sinkMode;

    private Vector3 lightningContactPos;
    private Vector3 debug;

    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        fireMode = RICFireMode.NULL;
        chargeCycleDone = false;

        lightningRenderer = GetComponentInChildren<LightningRenderer>();
        lightningRenderer.enabled = false;
    }

    private void FixedUpdate()
    {
        TargetFinder();
    }

    private void Update()
    {
        Fire();
    }

    private void TargetFinder()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, 100f, floorMask, QueryTriggerInteraction.Ignore))
        {
            debug = hit.point;
            Collider[] scans = Physics.OverlapSphere(hit.point, sphereCastRadius, ricScanMask);
            marker.localScale = Vector3.one * sphereCastRadius;
            marker.position = hit.point;

            if (scans.Length > 0)
            {
                lightningContactPos = scans[0].ClosestPointOnBounds(hit.point);
                currentTarget = scans[0].transform;
                ClassifyTarget();
            }
            else
            {
                currentTarget = null;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(debug, sphereCastRadius);
        
        if(currentTarget != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(currentTarget.position, sphereCastRadius);
        }
    }

    private void Fire()
    {
        if (currentTarget == null)
        {
            lightningRenderer.enabled = false;
            return;
        }

        if(Input.GetButton("Fire1") && !chargeCycleDone)
        {
            switch (fireMode)
            {
                case RICFireMode.NULL:
                    lightningRenderer.enabled = false;
                    return;

                case RICFireMode.DAMAGE:
                    lightningRenderer.emitterPosition = ricBarrelTransform.position;
                    lightningRenderer.receiverPosition = lightningContactPos;
                    lightningRenderer.color = damageMode;
                    lightningRenderer.enabled = true;
                    chargeCycleDone = false;

                    break;
                case RICFireMode.SOURCE:
                    lightningRenderer.emitterPosition = lightningContactPos;
                    lightningRenderer.receiverPosition = ricBarrelTransform.position;
                    lightningRenderer.color = sourceMode;
                    lightningRenderer.enabled = true;

                    if (!currentPowerSrc.Discharge(chargeTransferRate))
                    {
                        Debug.Log("Charge cycle done");
                        chargeCycleDone = true;
                        lightningRenderer.enabled = false;
                    }

                    break;
                case RICFireMode.SINK:
                    lightningRenderer.emitterPosition = lightningContactPos;
                    lightningRenderer.receiverPosition = ricBarrelTransform.position;
                    lightningRenderer.color = sinkMode;
                    lightningRenderer.enabled = true;

                    if (!currentPowerSrc.Charge(chargeTransferRate))
                    {
                        Debug.Log("Charge cycle done");
                        chargeCycleDone = true;
                        lightningRenderer.enabled = false;
                    }

                    break;
            }
        }
        else
        {
            lightningRenderer.enabled = false;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            chargeCycleDone = false;
        }
    }

    private void ClassifyTarget()
    {
        fireMode = RICFireMode.NULL;

        if (currentTarget.GetComponent<Enemy_Master>() != null) fireMode = RICFireMode.DAMAGE;
        else
        {
            currentPowerSrc = currentTarget.GetComponent<PowerSource>();
            if (currentPowerSrc != null)
            {
                if (currentPowerSrc.empty) fireMode = RICFireMode.SINK;
                else fireMode = RICFireMode.SOURCE;
            }
        }
    }

    /*private void FixedUpdate()
    {
        CheckFire();
    }

    private void Update()
    {
        Fire();
    }

    private void CheckFire()
    {
        if (!ammoBox.HasSufficientAmmo(weaponData.ammoType, weaponData.fireCostPrimary))
        {
            if (ammoBox.GetAmmoData(weaponData.ammoType).ammoInBag == 0) return;
            else Reload();
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        

        if (Physics.Raycast(ray, out hit, 100f))
        {
            Collider[] scans = Physics.OverlapSphere(hit.point, sphereCastRadius, ricScanMask);
            marker.localScale = Vector3.one * sphereCastRadius;
            marker.position = hit.point;

            if(scans.Length > 0)
            {
                lightningRenderer.emitterPosition = scans[0].ClosestPointOnBounds(hit.point);
                currentTarget = scans[0].transform.root;
                canFire = true;
            }
            else
            {
                currentTarget = null;
                canFire = false;
            }
        }
    }

    private IEnumerator CoolDown()
    {
        inCoolDown = true;

        while(currentHeat > maxHeat * 0.5f)
        {
            yield return null;
        }

        inCoolDown = false;
    }

    private void Fire()
    {
        if (Input.GetButton("Fire1") && canFire && !inCoolDown)
        {
            HandleTarget();
            currentPower += powerUpSpeed * Time.deltaTime;
            currentHeat += heatUpSpeed * Time.deltaTime;

            if(currentHeat > maxHeat)
            {
                currentHeat = maxHeat;
                StartCoroutine(CoolDown());
            }
        }
        else
        {
            lightningRenderer.enabled = false;

            currentPower -= powerDownSpeed * Time.deltaTime;
            currentHeat -= coolDownSpeed * Time.deltaTime;

            currentPower = Mathf.Max(currentPower, 0);
            currentHeat = Mathf.Max(currentHeat, 0);
        }
    }

    //At this point the RIC is qualified to interact with target
    private void HandleTarget()
    {
        if (currentTarget.GetComponent<Enemy_Master>() != null) fireMode = RICFireMode.DAMAGE;
        else fireMode = RICFireMode.NULL;

        if (currentPower > powerThreshold)
        {
            currentPower = powerThreshold;
        }

        if (fireMode == RICFireMode.DAMAGE)
        {
            //assign custom properties to lightning renderer via a scriptable object profile

            lightningRenderer.enabled = true;
            lightningRenderer.throttle = currentPower * currentPower * currentPower;
            //lightningRenderer.boltLength = currentPower;
        }
        else
        {
            lightningRenderer.enabled = false;
        }
    }

    private void Reload()
    {
        Ammo ammo = ammoBox.GetAmmoData(weaponData.ammoType);
        ammo.ammoInClip = ammo.clipSize;
        ammo.ammoInBag -= ammo.clipSize;
        if (ammo.ammoInBag < 0) ammo.ammoInClip += ammo.ammoInBag;
    }*/
}
