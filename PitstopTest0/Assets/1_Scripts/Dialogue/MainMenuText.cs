using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Pitstop
{
    public class MainMenuText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI ownText = default;
        [SerializeField] string englishText = null;
        [SerializeField] string frenchText = null;

        public void ChangeTextLanguageTo(string language)
        {
            if (language == "French")
            {
                ownText.text = frenchText;
            }
            else if (language == "English")
            {
                ownText.text = englishText;
            }
        }
    }
}