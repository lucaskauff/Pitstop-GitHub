using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class LUD_DetectionTriggeredByAttention : MonoBehaviour
    {
        
        public bool isThePlayerNear = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "AttentionZone")
            {
                isThePlayerNear = true;

                if (!GetComponentInParent<LUD_NonDialogueReactions>().isOffended)
                {
                    CaptivationOfTheNative();
                }

            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "AttentionZone")
            {
                isThePlayerNear = false;
                UncaptivationOfTheNative();
            }
        }

        public void CaptivationOfTheNative()
        {
            GetComponentInParent<LUD_NativeHeartheSentence>().isCaptivated = true;
            GetComponentInParent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
        }

        public void UncaptivationOfTheNative()
        {
            GetComponentInParent<LUD_NativeHeartheSentence>().isCaptivated = false;
            GetComponentInParent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f);
        }
    }
}