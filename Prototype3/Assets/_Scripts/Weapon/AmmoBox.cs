using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBox : MonoBehaviour
{
    private Player_Master playerMaster;

    public AmmoType ammoType;

    public int clipSize;
    public int ammoQuantity;
    public int totalAmmo;

    public Text ammoQuantityText;
    public Text totalAmmoText;

    private void OnEnable()
    {
        Initialize();
        playerMaster.OnAmmoPickup += UpdateAmmoBox;
    }

    private void Initialize()
    {
        playerMaster = transform.root.gameObject.GetComponent<Player_Master>();
        UpdateUI();
    }

    private void UpdateAmmoBox(AmmoType type, int quantity)
    {
        if (!type.Equals(ammoType)) return;

        totalAmmo += quantity;

        UpdateUI();
    }

    public void Reload()
    {
        int amount = Mathf.Min(totalAmmo, clipSize - ammoQuantity);
        totalAmmo -= amount;
        ammoQuantity += amount;
        UpdateUI();
        Debug.Log("Reload Complete");
    }

    public void DecrementAmmo()
    {
        ammoQuantity--;
        UpdateUI();
    }

    private void UpdateUI()
    {
        ammoQuantityText.text = ammoQuantity.ToString();
        totalAmmoText.text = totalAmmo.ToString();
    }

    private void OnDisable()
    {
        playerMaster.OnAmmoPickup -= UpdateAmmoBox;
    }
}
