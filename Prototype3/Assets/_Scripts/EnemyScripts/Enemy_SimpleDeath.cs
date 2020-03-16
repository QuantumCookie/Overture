using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SimpleDeath : MonoBehaviour
{
    private Enemy_Master enemyMaster;

    public GameObject enemyExplosion;

    private void OnEnable()
    {
        Initialize();
        enemyMaster.OnEnemyDead += Die;
    }

    private void Initialize()
    {
        enemyMaster = GetComponent<Enemy_Master>();
    }

    private void Die()
    {
        Instantiate(enemyExplosion, transform.position, transform.rotation);
        gameObject.SetActive(false);
        //Destroy(this.gameObject);
    }

    private void OnDisable()
    {
        enemyMaster.OnEnemyDead -= Die;
    }
}
