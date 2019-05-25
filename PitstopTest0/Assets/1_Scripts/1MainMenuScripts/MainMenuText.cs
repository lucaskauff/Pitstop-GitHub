using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Pitstop
{
    public class MainMenuText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI ownText = default;
        [SerializeField, TextArea(1, 10)] string englishText = null;
        [SerializeField, TextArea(1, 10)] string frenchText = null;

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