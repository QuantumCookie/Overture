using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    public float destroyAfter = 5f;

    void Start()
    {
        Destroy(gameObject, destroyAfter);
    }

}
