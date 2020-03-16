using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    private Player_Master playerMaster;
    private GameManager_Master gameManagerMaster;

    public float currentHealth = 100f;
    public float maxHealth = 100f;


    private void OnEnable()
    {
        Initialize();

        playerMaster.OnHealthGain += HealthGain;
        playerMaster.OnHealthDeduct += HealthLost;
    }

    private void Initialize()
    {
        gameManagerMaster = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Master>();
        playerMaster = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Master>();
    }

    private void HealthGain(float delta)
    {
        currentHealth = Mathf.Min(currentHealth + delta, maxHealth);
    }

    private void HealthLost(float delta)
    {
        currentHealth = Mathf.Max(currentHealth - delta, 0);
        if (currentHealth == 0 && !gameManagerMaster.isGameOver) gameManagerMaster.CallGameOverEvent();
    }

    private void OnDisable()
    {
        playerMaster.OnHealthGain -= HealthGain;
        playerMaster.OnHealthDeduct -= HealthLost;
    }
}
