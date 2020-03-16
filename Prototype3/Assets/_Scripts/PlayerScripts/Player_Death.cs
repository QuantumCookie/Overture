using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Death : MonoBehaviour
{
    public GameObject playerExplosion;

    private GameManager_Master gameManagerMaster;

    private void OnEnable()
    {
        Initialize();
        gameManagerMaster.OnGameOver += Die;
    }

    private void Initialize()
    {
        gameManagerMaster = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Master>();
    }

    private void Die()
    {
        Instantiate(playerExplosion, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        gameManagerMaster.OnGameOver -= Die;
    }
}
