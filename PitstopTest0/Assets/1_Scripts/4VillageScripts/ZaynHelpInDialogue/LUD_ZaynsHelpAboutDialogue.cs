using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class LUD_ZaynsHelpAboutDialogue : MonoBehaviour
    {
        [Header("Helps When Too much ?")]
        public int nbrOfInterrogation = 0;
        [SerializeField] int minNbrOfInterrogationForTriggerFirstHelp = 4;
        [SerializeField] int minNbrOfInterrogationForTriggerSecondHelp = 7;
        [SerializeField] int minNbrOfInterrogationForTriggerThirdHelp = 11;
        [SerializeField] int minNbrOfInterrogationForTriggerFourthHelp = 14;

        public DialogueTrigger firstDialogueOfHelps;
        public DialogueTrigger secondDialogueOfHelps;
        public DialogueTrigger thirdDialogueOfHelps;
        public DialogueTrigger fourthDialogueOfHelps;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
            if (nbrOfInterrogation >= minNbrOfInterrogationForTriggerFourthHelp)
            {
                fourthDialogueOfHelps.TriggerDialogueDirectly();
            }
            else if (nbrOfInterrogation >= minNbrOfInterrogationForTriggerThirdHelp)
            {
                thirdDialogueOfHelps.TriggerDialogueDirectly();
            }
            else if (nbrOfInterrogation >= minNbrOfInterrogationForTriggerSecondHelp)
            {
                secondDialogueOfHelps.TriggerDialogueDirectly();
            }
            else if (nbrOfInterrogation>=minNbrOfInterrogationForTriggerFirstHelp)
            {
                firstDialogueOfHelps.TriggerDialogueDirectly();
            }
            
        }
    }
}
