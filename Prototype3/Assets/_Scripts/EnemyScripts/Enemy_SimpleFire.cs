using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_SimpleFire : MonoBehaviour
{
    public float damage = 10f;
    public float projectileSpeed = 20f;
    public float range = 7f;
    public LayerMask playerLayer;
    public Transform projectileSpawn;

    public float fireDelay = 1f;
    private float lastFire;

    public GameObject enemyProjectile;
    private bool facingPlayer;

    private Enemy_Master enemyMaster;
    private GameManager_Master gameManagerMaster;

    private bool forceDisableFire;

    private void OnEnable()
    {
        Initialize();
        enemyMaster.OnEnemyDead += DisableFire;
        gameManagerMaster.OnGameOver += DisableFire;
    }

    private void Initialize()
    {
        enemyMaster = GetComponent<Enemy_Master>();
        gameManagerMaster = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Master>();
    }

    private void Update()
    {
        CheckFacingPlayer();
        Fire();
    }

    private void Fire()
    {
        if (!facingPlayer || forceDisableFire) return;

        if (Time.time > lastFire + fireDelay)
        {
            GameObject g = Instantiate(enemyProjectile, projectileSpawn.position, projectileSpawn.rotation);
            g.GetComponent<Projectile_Data>().damage = damage;
            g.GetComponent<Projectile_Data>().sourceTag = "Enemy";
            g.GetComponent<Projectile_Data>().targetTag = "Player";
            g.GetComponent<Projectile_Data>().speed = projectileSpeed;

            lastFire = Time.time;
        }
    }

    private void CheckFacingPlayer()
    {
        //facingPlayer = true;
        facingPlayer = Physics.Raycast(transform.position, transform.forward, range, playerLayer);
    }

    private void DisableFire()
    {
        forceDisableFire = true;
    }

    private void OnDisable()
    {
        enemyMaster.OnEnemyDead -= DisableFire;
        gameManagerMaster.OnGameOver -= DisableFire;
    }
}
