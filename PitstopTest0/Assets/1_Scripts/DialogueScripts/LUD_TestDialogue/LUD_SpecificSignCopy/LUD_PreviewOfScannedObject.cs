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
                crystalButtonInDialogueWheel.GetComponent<LUD_TestButton>().valueOfTheWord = valueOfWord; //temporairement, vu qu'on teste avec les natifs
                crystalButtonInDialogueWheel.GetComponent<Button>().interactable = true;
                crystalButtonInDialogueWheel.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                crystalButtonInDialogueWheel.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);

            }
            else
            {
                crystalButtonInDialogueWheel.GetComponent<LUD_TestButton>().valueOfTheWord = valueOfWord; //dans le pire des cas, le natif ne comprendra rien
                crystalButtonInDialogueWheel.GetComponent<Button>().interactable = false;
                crystalButtonInDialogueWheel.GetComponent<Image>().color = new Color (1,1,1,transparenceyOfSpriteIfNotWord);
                crystalButtonInDialogueWheel.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, transparenceyOfSpriteIfNotWord);
            }
        }

    }
}
