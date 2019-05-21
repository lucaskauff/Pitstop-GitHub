using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class LUD_TriggerTheFirstDialogue : MonoBehaviour
    {

        public DialogueTrigger dialogueBox;
        bool hasBeentriggeredOnce = false;



        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.gameObject.tag == "Player" && !hasBeentriggeredOnce)
            {
                dialogueBox.TriggerDialogueDirectly();
                FindObjectOfType<LUD_NativeBehaviourOnMiniBossScene>().StartCoroutine("WaitBeforeFlyingAway");

                //Debug.Log("FindObjectOfType<EerickBehaviour>().playerHasTriggeredNative = " + FindObjectOfType<EerickBehaviour>().playerHasTriggeredNative);
                FindObjectOfType<EerickBehaviour>().playerHasTriggeredNative = true;
                FindObjectOfType<EerickBehaviourTestLUD>().playerHasTriggeredNative = true;

                hasBeentriggeredOnce = true;


            }

        }

        

    }

   
}
