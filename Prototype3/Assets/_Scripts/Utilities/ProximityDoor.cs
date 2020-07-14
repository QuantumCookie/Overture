using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityDoor : MonoBehaviour
{
    private Animator doorController;

    private void OnEnable()
    {
        GetComponent<BoxCollider>().enabled = true;
        doorController = GetComponent<Animator>();
        doorController.SetBool("EntityNearby", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            doorController.SetBool("EntityNearby", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            doorController.SetBool("EntityNearby", false);
        }
    }

    private void OnDisable()
    {
        GetComponent<BoxCollider>().enabled = false;
    }
}
