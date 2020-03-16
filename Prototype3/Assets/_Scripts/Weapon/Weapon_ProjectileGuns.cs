using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_ProjectileGuns : MonoBehaviour
{
    private Player_AmmoBox ammoBox;
    private Player_ProjectilePool projectilePool;
    private Weapon_Data weaponData;
    private Weapon_Master weaponMaster;

    public GameObject projectile;
    public GameObject muzzleEffects;
    public Transform[] projectileSpawn;
    public WeaponMode weaponMode;

    public string projectileName;

    private float fireRate;
    private float fireDamage;
    private int fireCost;
    private string fireButton;
    private float lastFire;

    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        ammoBox = transform.root.GetComponent<Player_AmmoBox>();
        projectilePool = ammoBox.GetComponent<Player_ProjectilePool>();
        weaponData = GetComponent<Weapon_Data>();
        weaponMaster = GetComponent<Weapon_Master>();

        if(weaponMode == WeaponMode.PRIMARY)
        {
            fireRate = weaponData.fireRatePrimary;
            fireCost = weaponData.fireCostPrimary;
            fireDamage = weaponData.damagePrimary;
            fireButton = "Fire1";
        }
        else
        {
            fireRate = weaponData.fireRateSecondary;
            fireCost = weaponData.fireCostSecondary;
            fireDamage = weaponData.damageSecondary;
            fireButton = "Fire2";
        }
    }

    private void Update()
    {
        Fire();
    }

    private void Fire()
    {

        if (Time.time > lastFire + fireRate && Input.GetButton(fireButton) && ammoBox.HasSufficientAmmo(weaponData.ammoType, fireCost)
            && !weaponMaster.isReloading)
        {
            int i = Mathf.FloorToInt(Random.Range(0, projectileSpawn.Length - 1));
            //Debug.Log(weaponMode + " Pew!");

            if(projectile != null)
            {
                GameObject shot = projectilePool.GetObject(projectileName, projectileSpawn[i]);
                //GameObject shot = Instantiate(projectile, projectileSpawn[i].position, projectileSpawn[i].rotation);
                //shot.transform.position = projectileSpawn[i].position;
                //shot.transform.rotation = projectileSpawn[i].rotation;
                shot.GetComponent<Projectile_Data>().damage = fireDamage;
                shot.GetComponent<Projectile_Data>().sourceTag = "Player";
                shot.GetComponent<Projectile_Data>().targetTag = "Enemy";
            }

            if(muzzleEffects != null)
            {
                Instantiate(muzzleEffects, projectileSpawn[i]);
            }

            ammoBox.DeductAmmo(weaponData.ammoType, fireCost);

            lastFire = Time.time;
        }
    }

    private void OnDisable()
    {
        
    }
}
