using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pitstop
{
    public class LUD_ZaynsHelpAboutDialogue : MonoBehaviour
    {
        bool isLanguageOfTheGameenglish;

        [Header("Dialogue Wheels Systems")]
        [SerializeField] GameObject dialogueSystemEnglish = default;
        [SerializeField] GameObject dialogueSystemFrench = default;

        public bool isCrystaltextEnabled = false;

        [Header ("List of text EN")]
        [SerializeField] GameObject textMeEn = default;
        [SerializeField] GameObject textYouEn = default;
        [SerializeField] GameObject textSeeEn = default;
        [SerializeField] GameObject textGniackEn = default;

        [Header("List of text FR")]
        [SerializeField] GameObject textMeFr = default;
        [SerializeField] GameObject textYouFr = default;
        [SerializeField] GameObject textSeeFr = default;
        [SerializeField] GameObject textGniackFr = default;

        [Header("Helps When Too much ?")]
        public int nbrOfInterrogation = 0;
        [SerializeField] int minNbrOfInterrogationForTriggerFirstHelp = 2;
        [SerializeField] int minNbrOfInterrogationForTriggerSecondHelp = 5;
        [SerializeField] int minNbrOfInterrogationForTriggerThirdHelp = 8;
        [SerializeField] int minNbrOfInterrogationForTriggerFourthHelp = 10;

        public DialogueTrigger firstDialogueOfHelps;
        public DialogueTrigger secondDialogueOfHelps;
        public DialogueTrigger thirdDialogueOfHelps;
        public DialogueTrigger fourthDialogueOfHelps;

        // Start is called before the first frame update
        void Start()
        {
            isLanguageOfTheGameenglish = FindObjectOfType<GameManager>().languageSetToEnglish;

            if (isLanguageOfTheGameenglish)
            {
                dialogueSystemEnglish.SetActive(true);
                dialogueSystemFrench.SetActive(false);
            }
            else
            {
                dialogueSystemEnglish.SetActive(false);
                dialogueSystemFrench.SetActive(true);
            }
        }

        // Update is called once per frame
        void Update()
        {
            
            if (nbrOfInterrogation >= minNbrOfInterrogationForTriggerFourthHelp)
            {
               fourthDialogueOfHelps.TriggerDialogueDirectly();

                isCrystaltextEnabled = true;
            }
            else if (nbrOfInterrogation >= minNbrOfInterrogationForTriggerThirdHelp)
            {
                thirdDialogueOfHelps.TriggerDialogueDirectly();

                if (isLanguageOfTheGameenglish) textGniackEn.GetComponent<Text>().text = "To eat";
                else textGniackFr.GetComponent<Text>().text = "Manger";
            }
            else if (nbrOfInterrogation >= minNbrOfInterrogationForTriggerSecondHelp)
            {
                secondDialogueOfHelps.TriggerDialogueDirectly();

                if (isLanguageOfTheGameenglish) textSeeEn.GetComponent<Text>().text = "To See";
                else textSeeFr.GetComponent<Text>().text = "Voir";
            }
            else if (nbrOfInterrogation>=minNbrOfInterrogationForTriggerFirstHelp)
            {
                firstDialogueOfHelps.TriggerDialogueDirectly();

                if (isLanguageOfTheGameenglish) textMeEn.GetComponent<Text>().text = "Me";
                else textMeFr.GetComponent<Text>().text = "Moi";
                if (isLanguageOfTheGameenglish) textYouEn.GetComponent<Text>().text = "You";
                else textYouFr.GetComponent<Text>().text = "Toi";
            }
            
        }
    }
}
