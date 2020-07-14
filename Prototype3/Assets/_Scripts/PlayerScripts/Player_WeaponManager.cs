using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_WeaponManager : MonoBehaviour
{
    private List<Weapon_Master> weapons;

    private Player_Master playerMaster;
    private int currentWeaponID;

    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        playerMaster = GetComponent<Player_Master>();
        weapons = new List<Weapon_Master>();

        GetWeapons();
        DisableAllWeapons();

        if (weapons.Count > 0) weapons[currentWeaponID = 0].gameObject.SetActive(true);
        else currentWeaponID = -1;
    }

    private void Update()
    {
        CheckForWeaponToggle();
    }

    private void CheckForWeaponToggle()
    {
        if (currentWeaponID == -1) return;

        int previousWeaponID = currentWeaponID;

        if (Input.GetKeyDown(KeyCode.Q) || Input.mouseScrollDelta.y > 0) currentWeaponID++;
        else if (Input.GetKeyDown(KeyCode.E) || Input.mouseScrollDelta.y < 0) currentWeaponID--;
        else return;

        currentWeaponID = (currentWeaponID + weapons.Count) % weapons.Count;
        weapons[previousWeaponID].gameObject.SetActive(false);
        weapons[currentWeaponID].gameObject.SetActive(true);

        playerMaster.CallWeaponToggleEvent(currentWeaponID);
    }

    public Texture2D CurrentWeaponCursor()
    {
        if (currentWeaponID == -1) return null;
        return weapons[currentWeaponID].weaponObject.cursor;
    }

    private void GetWeapons()
    {
        weapons.AddRange(transform.GetComponentsInChildren<Weapon_Master>());
    }

    private void DisableAllWeapons()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        DisableAllWeapons();
    }
}
