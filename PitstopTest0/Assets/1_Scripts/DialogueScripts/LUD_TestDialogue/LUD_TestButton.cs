using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pitstop
{
    public class LUD_TestButton : MonoBehaviour
    {
        
        public int valueOfTheWord = 0;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void WhenClicked()
        {
            if (valueOfTheWord == -1)
            {
                FindObjectOfType<LUD_DialogueAttentionZoneSize>().ShoutEffect();
            }
            else
            {

                if (this.GetComponent<Button>().interactable)
                {
                    FindObjectOfType<LUD_DialogueManagement>().AddAWordToTheSentence(valueOfTheWord);
                }
                else
                {
                    FindObjectOfType<LUD_TestExploreWithMouseWheel>().AddAWordToTheSentence(valueOfTheWord);
                }

            }

        }
    }
}