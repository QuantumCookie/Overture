using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger_TogglePause : MonoBehaviour
{
    private GameManager_Master gameManagerMaster;

    private void OnEnable()
    {
        gameManagerMaster = GetComponent<GameManager_Master>();
    }

    private void Update()
    {
        if(!gameManagerMaster.isGameOver)
        {
            if (Input.GetKeyUp(KeyCode.Escape)) TogglePause();
        }
    }

    public void TogglePause()
    {
        if(!gameManagerMaster.isGameOver)
        {
            if (gameManagerMaster.isGamePaused) Time.timeScale = 1;
            else Time.timeScale = 0;

            gameManagerMaster.CallTogglePauseEvent();
        }
    }
}
