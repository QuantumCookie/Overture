using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSource : MonoBehaviour
{
    public bool empty;
    public float capacity = 20;
    public float power = 0;

    private float idleDischargeTime = 7f;

    public delegate void GeneralEvent();

    public event GeneralEvent powerEmpty;
    public event GeneralEvent powerFull;

    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        power = Mathf.Clamp(power, 0, capacity);
        empty = power < capacity;
    }

    public bool Discharge(float delta)
    {
        if (empty) return false;

        power -= delta;

        if(power < 0)
        {
            power = 0;
            empty = true;
            if(powerEmpty != null) powerEmpty();
            return false;
        }

        return true;
    }

    private void Update()
    {
        if(power > 0 && power < capacity)
        {
            if(empty)
            {
                power -= idleDischargeTime * Time.deltaTime;
                if (power < 0) power = 0;
            }
            else
            {
                power += idleDischargeTime * Time.deltaTime;
                if (power > capacity) power = capacity;
            }
        }
    }

    public bool Charge(float delta)
    {
        power += delta;

        if (power > capacity)
        {
            power = capacity;
            empty = false;
            if (powerFull != null) powerFull();
            return false;
        }

        return true;
    }

    private void OnDisable()
    {
        
    }
}
