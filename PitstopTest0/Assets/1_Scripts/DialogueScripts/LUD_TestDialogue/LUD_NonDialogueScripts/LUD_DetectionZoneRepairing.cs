using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class LUD_DetectionZoneRepairing : MonoBehaviour
    {
        public GameObject builderNative1;
        public GameObject builderNative2;
        private bool isBridgeRepaired = false;
        private bool isPlayerInside = false;

        public GameObject dialogueWhenBridgeIsRepaired;

        // Update is called once per frame
        private void Update()
        {
            if (isPlayerInside && builderNative1.GetComponent<LUD_NonDialogueReactions>().isArrivedToEast && builderNative1.GetComponent<LUD_NonDialogueReactions>().isArrivedToEast && !isBridgeRepaired)
            {
                isBridgeRepaired = true;

                FindObjectOfType<OpenTheGate>().BridgeReparation();
                dialogueWhenBridgeIsRepaired.GetComponent<DialogueTrigger>().TriggerDialogue();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                isPlayerInside = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                isPlayerInside = false;
            }
        }
    }
}
