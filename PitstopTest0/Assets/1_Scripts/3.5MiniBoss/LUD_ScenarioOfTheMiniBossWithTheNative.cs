using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class LUD_ScenarioOfTheMiniBossWithTheNative : MonoBehaviour
    {
        [SerializeField] float timeBeforeCommentingTheEndOfFight = 1f;
        public DialogueTrigger dialogueAfterTheFight;

        [SerializeField] DialogueManager dialogueManager = default;
        [SerializeField] GameObject dialogueWheelSystem = default;

        private bool isDialogueWheelAppeared = false;

        [SerializeField] GameObject native = default;
        [SerializeField] GameObject openingDialogueWheelTutorial = default;

        private void Update()
        {
            string code = dialogueManager.codeOfTheLastTriggeringSentence;

            if (code == "Return Of The Native After The Mini Boss")
            {
                FindObjectOfType<LUD_NativeBehaviourOnMiniBossScene>().isReturningFromForest = true;
            }
            
            
            else if (code == "Tutorial For Dialogue Wheel" )
            {
                if (!isDialogueWheelAppeared)
                {
                    dialogueWheelSystem.SetActive(true);
                    isDialogueWheelAppeared = true;
                }
                
                if (native.GetComponentInChildren<LUD_DetectionTriggeredByAttention>().isThePlayerNear)
                {
                    openingDialogueWheelTutorial.SetActive(true);
                }
                else
                {
                    openingDialogueWheelTutorial.SetActive(false);
                }

                

            }
        }



        public void EndOfTheFightConcequences()
        {
            StartCoroutine(WaitBeforeCommentingAfterTheFight());
        }

        IEnumerator WaitBeforeCommentingAfterTheFight()
        {
            yield return new WaitForSeconds(timeBeforeCommentingTheEndOfFight);
            dialogueAfterTheFight.TriggerDialogueDirectly();
            
        }
    }
}
