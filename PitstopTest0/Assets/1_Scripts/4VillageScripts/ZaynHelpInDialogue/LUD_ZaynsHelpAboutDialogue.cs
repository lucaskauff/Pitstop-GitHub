using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pitstop
{
    public class LUD_ZaynsHelpAboutDialogue : MonoBehaviour
    {
        bool isLanguageOfTheGameenglish;
        [SerializeField] GameManager gameManager = default;

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
            gameManager = FindObjectOfType<GameManager>();
            isLanguageOfTheGameenglish = gameManager.languageSetToEnglish;

            
        }

        // Update is called once per frame
        void Update()
        {
            isLanguageOfTheGameenglish = gameManager.languageSetToEnglish;

            CheckAndChangeLanguage();

            if (nbrOfInterrogation >= minNbrOfInterrogationForTriggerFourthHelp)
            {
               fourthDialogueOfHelps.TriggerDialogueDirectly();

                isCrystaltextEnabled = true;
            }
            else if (nbrOfInterrogation >= minNbrOfInterrogationForTriggerThirdHelp)
            {
                thirdDialogueOfHelps.TriggerDialogueDirectly();

                textGniackEn.GetComponent<Text>().text = "To eat";
                textGniackFr.GetComponent<Text>().text = "Manger";
            }
            else if (nbrOfInterrogation >= minNbrOfInterrogationForTriggerSecondHelp)
            {
                secondDialogueOfHelps.TriggerDialogueDirectly();

                textSeeEn.GetComponent<Text>().text = "To See";
                textSeeFr.GetComponent<Text>().text = "Voir";
            }
            else if (nbrOfInterrogation>=minNbrOfInterrogationForTriggerFirstHelp)
            {
                firstDialogueOfHelps.TriggerDialogueDirectly();

                textMeEn.GetComponent<Text>().text = "Me";
                textMeFr.GetComponent<Text>().text = "Moi";
                textYouEn.GetComponent<Text>().text = "You";
                textYouFr.GetComponent<Text>().text = "Toi";
            }
            
        }

        void CheckAndChangeLanguage()
        {
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
    }
}
