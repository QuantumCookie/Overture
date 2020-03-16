using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 200;
    public int currentHealth;

    private bool isDead = false;
    private bool isHurt = false;

    //Health UI
    public Image healthBar;
    public float minWidth, maxWidth;

    //Hurt UI Animation
    public Image hurtUI;
    public Color hurtUIColor;
    private float fadeSpeed = 5f;

    //Player Death
    public GameObject playerExplosion;

    void Start()
    {
        currentHealth = maxHealth;

        UpdateHealthUI();
    }

    void Update()
    {
        HurtUIBlink();
    }

    private void HurtUIBlink()
    {
        if(isHurt)
        {
            isHurt = false;
            hurtUI.color = hurtUIColor;
        }
        hurtUI.color = Color.Lerp(hurtUI.color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    public void ApplyDamage(int damage)
    {
        if(!isDead)//Do damage animation and sounds
        {
            currentHealth -= damage;
            UpdateHealthUI();
            isHurt = true;

            if (currentHealth <= 0) Die();
        }
    }

    private void UpdateHealthUI()
    {
        healthBar.rectTransform.localScale = new Vector3(((float)currentHealth) / maxHealth, 1f, 1f);
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Dead!");
        gameObject.GetComponent<Player_Movement>().enabled = false;

        Destroy(gameObject);
        Instantiate(playerExplosion, transform.position, transform.rotation);

        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Master>().CallGameOverEvent();
    }
}
