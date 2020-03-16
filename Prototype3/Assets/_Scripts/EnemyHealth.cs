using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100;
    private float health;

    public int scoreValue = 10;

    //private GameManager_EnemyManager enemyManager;

    public GameObject explosionFX;

    private void OnEnable()
    {
        health = maxHealth;
        //enemyManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_EnemyManager>();
    }

    public void ApplyDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            health = 0;
            Die();
        }
    }

    private void Die()
    {
        Instantiate(explosionFX, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Debug.Log("Disabled");
    }
}
