using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class LUD_ChangeTheMusicOnMiniBoss : MonoBehaviour
    {
        bool hasBeenTriggered = false;
        [SerializeField] DialogueManager dialogueManager = default;
        [SerializeField] AudioSource fightMusic = default;

        [SerializeField] float timeOffadeOut = 2f;
        [SerializeField] float timeOffadeIn = 2f;

        float originalVolumeOfChillMusic;
        float originalVolumeOfFightMusic;

        [SerializeField] DialogueTrigger nextDialogueTrigger = default;

        // Start is called before the first frame update
        void Start()
        {
            originalVolumeOfChillMusic = GetComponent<AudioSource>().volume;
            originalVolumeOfFightMusic = fightMusic.GetComponent<AudioSource>().volume;
        }

        // Update is called once per frame
        void Update()
        {
            if (dialogueManager.codeOfTheLastTriggeringSentence == "StartFightMusic" && !hasBeenTriggered)
            {
                nextDialogueTrigger.TriggerDialogueDirectly();
                StartCoroutine(MusicTransition());
                hasBeenTriggered = true;

            }
        }

        IEnumerator MusicTransition()
        {
            float nbrOfStep = 20f;
            
            for (int i=0 ; i<nbrOfStep ; i++)
            {
                GetComponent<AudioSource>().volume -= originalVolumeOfChillMusic / nbrOfStep;
                yield return new WaitForSeconds(timeOffadeOut / nbrOfStep);
            }
            
            GetComponent<AudioSource>().volume = 0f;
            GetComponent<AudioSource>().Stop();

            
                        

            
            fightMusic.volume = 0f;
            fightMusic.mute = false;

            for (int i = 0; i < nbrOfStep; i++)
            {
                fightMusic.volume += originalVolumeOfFightMusic / nbrOfStep;
                yield return new WaitForSeconds(timeOffadeIn / nbrOfStep);
            }
            

            fightMusic.volume = originalVolumeOfFightMusic;

            //nextDialogueTrigger.TriggerDialogueDirectly();
            //yield return null;

        }
    }
}
