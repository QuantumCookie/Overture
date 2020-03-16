using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SimpleMelee : MonoBehaviour
{
    private Player_Master playerMaster;
    private GameManager_Master gameManagerMaster;

    private bool playerInRange = false;
    public int attackDamage = 20;
    public float attackRange = 3f;
    private float attackRate = 1f;
    private float lastAttack;

    private void OnEnable()
    {
        Initialize();
        gameManagerMaster.OnGameOver += DisableThis;
    }

    private void Initialize()
    {
        gameManagerMaster = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Master>();
        playerMaster = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Master>();
    }

    private void Update()
    {
        AttackPlayerIfInRange();
    }

    private void FixedUpdate()
    {
        CheckPlayerProximity();
    }

    void AttackPlayerIfInRange()
    {
        if (playerInRange && !gameManagerMaster.isGameOver)
        {
            if (Time.time > lastAttack + attackRate)
            {
                playerMaster.CallHealthDeductEvent(attackDamage);
                //Debug.Log("Whack!");
                lastAttack = Time.time;
            }
        }
    }

    private void CheckPlayerProximity()
    {
        if (Physics.CheckSphere(transform.position, attackRange, LayerMask.GetMask("Player")))
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
    }

    private void DisableThis()
    {
        this.enabled = false;
    }

    private void OnDisable()
    {
        gameManagerMaster.OnGameOver -= DisableThis;
    }
}
