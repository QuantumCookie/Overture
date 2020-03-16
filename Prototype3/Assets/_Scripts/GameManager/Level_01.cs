using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level_01 : MonoBehaviour
{
    private GameManager_Master gameManagerMaster;
    private Player_Master  playerMaster;

    public GameObject HUD;
    public CanvasGroup fadeIn;
    private float fadeInVelocity;
    public float fadeInTime = 5f;

    public TextMeshProUGUI dialogue;
    public Image dialogueBackground;
    public GameObject forceField;

    private void Start() 
    {
        StartCoroutine(Initialize());
    }

    private IEnumerator Initialize()
    {
        gameManagerMaster = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Master>();
        playerMaster = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Master>();
  
        HUD.SetActive(false);
        dialogue.SetText("");   

        playerMaster.GetComponent<Player_Movement>().enabled = false;
        playerMaster.GetComponent<Player_WeaponManager>().enabled = false;
        gameManagerMaster.GetComponent<GameManager_Cursor>().UpdateCursor();

        yield return FadeIn();

        //yield return Speak("UNKNOWN: Hey wake up! Ugh voice recognition's supposed to work dammit. C'mon quick wake up!!!", 2f, 5f);
        //yield return Speak("UNKNOWN: Just a sec. Let me remove the locks on your actuator system. You should be able to move soon", 0, 7f);

        playerMaster.GetComponent<Player_Movement>().enabled = true;

        //yield return Speak("UNKNOWN: Thrusters active. Now get the hell out of there!", 0, 5);
        //yield return Speak("Oh. The force field. Yeah. Right", 0, 2);

        forceField.SetActive(false);

        yield return Speak("UNKNOWN: Also, more importantly, don't let them get you", 3, 3);

        yield return null;
    }

    public IEnumerator Speak(string d, float initialDelay, float afterDelay)
    {
        yield return new WaitForSeconds(initialDelay);
        dialogue.SetText(d);
        dialogueBackground.enabled = true;
        yield return new WaitForSeconds(afterDelay);
        dialogue.SetText("");
        dialogueBackground.enabled = false;
    }

    IEnumerator FadeIn()
    {
        float t = 0;
        while (t < 1)
        {
            fadeIn.alpha = Mathf.Lerp(1, 0, t * t);
            t += Time.deltaTime / fadeInTime;
            yield return null;
        }
    }
}
