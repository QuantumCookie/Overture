using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    /*public int attackDamage = 10;
    public float attackRange = 2f;*/

    private GameObject player;
    private PlayerHealth playerHealth;
    private GameManager_Master gameManagerMaster;
    private NavMeshAgent nav;

    /*private bool playerInRange = false;
    private float attackRate = 1f;
    private float lastAttack;*/

    private void OnEnable()
    {
        Initialize();

        gameManagerMaster.OnGameOver += DisableNavigation;
    }

    private void Initialize()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        //playerHealth = player.GetComponent<PlayerHealth>();
        gameManagerMaster = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Master>();
    }

    private void Update()
    {
        SetNavDestination();
        //AttackPlayerIfInRange();
    }

    /*private void FixedUpdate()
    {
        CheckPlayerProximity();
    }*/

    private void SetNavDestination()
    {
        if (nav.enabled)
            nav.SetDestination(player.transform.position);
    }

    private void DisableNavigation()
    {
        nav.enabled = false;
    }

    /*void AttackPlayerIfInRange()
    {
        if(playerInRange)
        {
            if (Time.time > lastAttack + attackRate)
            {
                playerHealth.ApplyDamage(attackDamage);
                lastAttack = Time.time;
            }
        }
    }

    private void CheckPlayerProximity()
    {
        if(Physics.CheckSphere(transform.position, attackRange, LayerMask.GetMask("Player")))
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
    }*/

    private void OnDisable()
    {
        gameManagerMaster.OnGameOver -= DisableNavigation;
    }
}
