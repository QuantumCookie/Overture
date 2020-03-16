using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    private Enemy_Master enemyMaster;

    private float maxHealth = 50f;
    private float currentHealth = 50f;

    private void OnEnable()
    {
        Initialize();
        enemyMaster.OnHealthDeducted += TakeDamage;
    }

    private void Initialize()
    {
        currentHealth = maxHealth;

        enemyMaster = GetComponent<Enemy_Master>();
    }

    private void TakeDamage(float delta)
    {
        currentHealth = Mathf.Max(currentHealth - delta, 0);
        if (currentHealth == 0) enemyMaster.CallEnemyDeadEvent();

        Debug.Log("Health deducted");
    }

    private void OnDisable()
    {
        enemyMaster.OnHealthDeducted -= TakeDamage;
    }
}
