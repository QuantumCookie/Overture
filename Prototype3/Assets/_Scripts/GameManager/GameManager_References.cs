using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_References : MonoBehaviour
{
    public static GameManager_References instance;

    public GameManager_Master gameManagerMaster
    {
        get
        {
            if (gameManagerMaster == null) gameManagerMaster = GetComponent<GameManager_Master>();
            return gameManagerMaster;
        }
        set
        {

        }
    }

    public Player_Master playerMaster
    {
        get
        {
            if (playerMaster == null) playerMaster = GetComponent<Player_Master>();
            return playerMaster;
        }
        set
        {

        }
    }

    public static GameManager_References _gameManager
    {
        get
        {
            return instance;
        }
    }



    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        gameManagerMaster = GetComponent<GameManager_Master>();
        playerMaster = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Master>();
    }
}
