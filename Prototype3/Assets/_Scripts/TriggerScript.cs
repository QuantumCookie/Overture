using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    public GameObject level_01;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
            StartCoroutine(OnPlayerEnter());
    }

    private IEnumerator OnPlayerEnter()
    {
        yield return level_01.GetComponent<Level_01>().Speak("Door's locked? Try looking for another way.", 0, 4);
    }
}
