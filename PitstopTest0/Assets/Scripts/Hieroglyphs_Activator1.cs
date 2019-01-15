using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hieroglyphs_Activator1 : MonoBehaviour
{
    [SerializeField]
    DialogueManager diaMan;
    [SerializeField]
    DialogueTrigger diaTrig;

    bool playerRead = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            diaMan.DisplayNextSentence();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(playerRead == false)
        {
            playerRead = true;
            diaTrig.TriggerDialogue();
        }        
    }
}