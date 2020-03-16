using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_SimpleMovement : MonoBehaviour
{
    private Projectile_Data pData;

    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        pData = GetComponent<Projectile_Data>();
    }

    private void Update()
    {
        transform.Translate(transform.forward * pData.speed * Time.deltaTime, Space.World);
    }
}
