using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pitstop
{
    public class LUD_PreviewOfScannedObject : MonoBehaviour
    {

        public GameObject crystalButtonInDialogueWheel;

        [SerializeField, Range(0, 1)]
        float transparenceyOfSpriteIfNotWord = 0.5f;

        public void ChangePreviewCrystalInDialogueWheel(Sprite _sprite, int valueOfWord, bool isAWord)
        {
            crystalButtonInDialogueWheel.transform.GetChild(0).GetComponent<Image>().sprite = _sprite;



            if (isAWord)
            {
                if (FindObjectOfType<LUD_ZaynsHelpAboutDialogue>().isCrystaltextEnabled)
                {
                    if (FindObjectOfType<GameManager>().languageSetToEnglish)
                    {
                        if (valueOfWord == 41) crystalButtonInDialogueWheel.GetComponentInChildren<Text>().text = "To Repair";
                        if (valueOfWord == 43) crystalButtonInDialogueWheel.GetComponentInChildren<Text>().text = "Danger";
                        if (valueOfWord == 47) crystalButtonInDialogueWheel.GetComponentInChildren<Text>().text = "Child";
                    }
                    else
                    {
                        if (valueOfWord == 41) crystalButtonInDialogueWheel.GetComponentInChildren<Text>().text = "Reparer";
                        if (valueOfWord == 43) crystalButtonInDialogueWheel.GetComponentInChildren<Text>().text = "Danger";
                        if (valueOfWord == 47) crystalButtonInDialogueWheel.GetComponentInChildren<Text>().text = "Enfant";
                    }
                }

                


                crystalButtonInDialogueWheel.GetComponent<LUD_TestButton>().valueOfTheWord = valueOfWord; //temporairement, vu qu'on teste avec les natifs
                crystalButtonInDialogueWheel.GetComponent<Button>().interactable = true;
                crystalButtonInDialogueWheel.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                crystalButtonInDialogueWheel.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);

            }
            else
            {
                crystalButtonInDialogueWheel.GetComponentInChildren<Text>().text = "";

                crystalButtonInDialogueWheel.GetComponent<LUD_TestButton>().valueOfTheWord = valueOfWord; //dans le pire des cas, le natif ne comprendra rien
                crystalButtonInDialogueWheel.GetComponent<Button>().interactable = false;
                crystalButtonInDialogueWheel.GetComponent<Image>().color = new Color (1,1,1,transparenceyOfSpriteIfNotWord);
                crystalButtonInDialogueWheel.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, transparenceyOfSpriteIfNotWord);
            }
        }

    }
}
