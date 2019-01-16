using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hieroglyphs_Activator : MonoBehaviour
{
    [SerializeField]
    DialogueManager diaMan;
    [SerializeField]
    DialogueTrigger diaTrig;

    bool playerReading = false;

    private void Update()
    {
        if (playerReading && Input.GetKeyDown(KeyCode.E))
        {
            diaMan.DisplayNextSentence();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(playerReading == false)
        {
            playerReading = true;
            diaTrig.TriggerDialogue();
        }
    }
}