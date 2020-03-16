using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Cursor : MonoBehaviour
{
    private GameManager_Master gameManagerMaster;
    private Player_Master playerMaster;
    private Player_WeaponManager weaponManager;

    public Texture2D mainCursor;

    private void OnEnable()
    {
        Initialize();
        gameManagerMaster.OnTogglePause += UpdateCursor;
        playerMaster.OnWeaponToggle += UpdateCursor;
        gameManagerMaster.OnGameOver += SetDefaultCursor;

        UpdateCursor();
    }

    private void Initialize()
    {
        gameManagerMaster = GetComponent<GameManager_Master>();
        weaponManager = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_WeaponManager>();
        playerMaster = weaponManager.GetComponent<Player_Master>();
    }

    public void UpdateCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        if(gameManagerMaster.isGamePaused)
        {
            SetDefaultCursor();
        }
        else if(!weaponManager.enabled)
        {
            SetNullCursor();
        }
        else
        {
            Texture2D cursor = weaponManager.CurrentWeaponCursor();
            Cursor.SetCursor(cursor, new Vector2(cursor.width / 2, cursor.height / 2), CursorMode.Auto);
        }
    }

    private void UpdateCursor(int weaponId)
    {
        UpdateCursor();
    }

    private void SetDefaultCursor()
    {
        Cursor.SetCursor(mainCursor, Vector2.zero, CursorMode.Auto);
    }

    private void SetNullCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        gameManagerMaster.OnTogglePause -= UpdateCursor;
        playerMaster.OnWeaponToggle -= UpdateCursor;
        gameManagerMaster.OnGameOver -= SetDefaultCursor;
    }
}
