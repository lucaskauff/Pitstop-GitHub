using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class LUD_MusicTransition : MonoBehaviour
    {
        [SerializeField] bool willBeTriggerByDialogue;
        [SerializeField] string codeOfTriggeringSentence;

        bool hasBeenTriggered = false;
        [SerializeField] DialogueManager dialogueManager = default;
        [SerializeField] AudioSource nextMusic = default;

        [SerializeField] float timeOffadeOut = 2f;
        [SerializeField] float timeOffadeIn = 2f;

        float originalVolumeOfChillMusic;
        float originalVolumeOfFightMusic;

        [Header("Dialogue after transition")]
        [SerializeField] bool willTriggerANewDialogue;
        [SerializeField] DialogueTrigger nextDialogueTrigger = default;

        // Start is called before the first frame update
        void Start()
        {
            originalVolumeOfChillMusic = GetComponent<AudioSource>().volume;
            originalVolumeOfFightMusic = nextMusic.GetComponent<AudioSource>().volume;
        }

        // Update is called once per frame
        void Update()
        {
            if (willBeTriggerByDialogue)
            {
                if (dialogueManager.codeOfTheLastTriggeringSentence == codeOfTriggeringSentence && !hasBeenTriggered)
                {
                    if (willTriggerANewDialogue) nextDialogueTrigger.TriggerDialogueDirectly();
                    StartCoroutine(MusicTransition());
                    hasBeenTriggered = true;
                }
            }
        }

        public IEnumerator MusicTransition()
        {
            Debug.Log("Music transition");

            float nbrOfStep = 20f;

            //TRANSITION ONE AFTER AN OTHER
            
            for (int i=0 ; i<nbrOfStep ; i++)
            {
                GetComponent<AudioSource>().volume -= originalVolumeOfChillMusic / nbrOfStep;
                yield return new WaitForSeconds(timeOffadeOut / nbrOfStep);
            }
            
            GetComponent<AudioSource>().volume = 0f;
            GetComponent<AudioSource>().mute = true;





            nextMusic.volume = 0f;
            nextMusic.mute = false;

            for (int i = 0; i < nbrOfStep; i++)
            {
                nextMusic.volume += originalVolumeOfFightMusic / nbrOfStep;
                yield return new WaitForSeconds(timeOffadeIn / nbrOfStep);
            }


            nextMusic.volume = originalVolumeOfFightMusic;

            //nextDialogueTrigger.TriggerDialogueDirectly();
            //yield return null;
            

            /*
            //TRANSITION A THE SAME TIME\\
            fightMusic.volume = 0f;
            fightMusic.mute = false;

            for (int i = 0; i < nbrOfStep; i++)
            {
                GetComponent<AudioSource>().volume -= originalVolumeOfChillMusic / nbrOfStep;
                fightMusic.volume += originalVolumeOfFightMusic / nbrOfStep;
                yield return new WaitForSeconds(timeOffadeOut / nbrOfStep);
                
            }

            GetComponent<AudioSource>().volume = 0f;
            GetComponent<AudioSource>().Stop();

            fightMusic.volume = originalVolumeOfFightMusic;
            */

        }
    }
}
