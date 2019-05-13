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


        private void Update()
        {
            string code = dialogueManager.codeOfTheLastTriggeringSentence;

            if (code == "Return Of The Native After The Mini Boss")
            {
                FindObjectOfType<LUD_NativeBehaviourOnMiniBossScene>().isReturningFromForest = true;
            }
            
            
            else if (dialogueManager.codeOfTheLastTriggeringSentence == "Tutorial For Dialogue Wheel" && !isDialogueWheelAppeared)
            {
                
                dialogueWheelSystem.SetActive(true);

                //afficher le tuto bouton

                isDialogueWheelAppeared = true;

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
