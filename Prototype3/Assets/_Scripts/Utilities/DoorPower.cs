using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPower : MonoBehaviour
{
    [SerializeField] private PowerSource doorPowerSource;
    private ProximityDoor doorProximityCheck;

    private void OnEnable()
    {
        Initialize();
        doorPowerSource.powerEmpty += DeactivateDoor;
        doorPowerSource.powerFull += ActivateDoor;
    }

    private void Start()
    {
        if (doorPowerSource.empty) DeactivateDoor();
        else ActivateDoor();
    }

    private void Initialize()
    {
        doorProximityCheck = GetComponent<ProximityDoor>();
    }
    
    private void ActivateDoor()
    {
        doorProximityCheck.enabled = true;
    }

    private void DeactivateDoor()
    {
        doorProximityCheck.enabled = false;
    }

    private void OnDisable()
    {
        doorPowerSource.powerEmpty -= DeactivateDoor;
        doorPowerSource.powerFull -= ActivateDoor;
    }
}
