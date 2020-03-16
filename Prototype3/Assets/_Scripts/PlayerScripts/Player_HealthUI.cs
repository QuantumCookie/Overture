using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_HealthUI : MonoBehaviour
{
    private Player_Health playerHealth;
    private Player_Master playerMaster;
    private GameManager_Master gameManagerMaster;

    //Health UI
    public Image healthBar;

    //Hurt UI Animation
    public Image hurtUI;
    public Color hurtUIColor;
    private float fadeSpeed = 7f;

    private void OnEnable()
    {
        Initialize();
        playerMaster.OnHealthDeduct += TakeDamage;
        gameManagerMaster.OnGameOver += ClearOnDeath;
    }

    private void Initialize()
    {
        playerHealth = GetComponent<Player_Health>();
        playerMaster = GetComponent<Player_Master>();
        gameManagerMaster = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Master>();
        UpdateHealthUI();
    }

    private void Update()
    {
        HurtUIBlink();
        UpdateHealthUI();
    }

    private void TakeDamage(float delta)
    {
        hurtUI.color = hurtUIColor;
    }

    private void HurtUIBlink()
    {
        hurtUI.color = Color.Lerp(hurtUI.color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    private void UpdateHealthUI()
    {
        healthBar.rectTransform.localScale = new Vector3(playerHealth.currentHealth / playerHealth.maxHealth, 1f, 1f);
    }

    private void ClearOnDeath()
    {
        hurtUI.color = Color.clear;
    }

    private void OnDisable()
    {
        playerMaster.OnHealthDeduct -= TakeDamage;
        gameManagerMaster.OnGameOver -= ClearOnDeath;
    }
}
