using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class LUD_DetectionZoneEast : MonoBehaviour
    {

        public GameObject butcherNative;
        [SerializeField] bool isPlayerInside = false;
        [SerializeField] bool isShowWhereIsEllyaAppeared = false;
        public GameObject EndOfSceneTextTrigger;

        private void Update()
        {
           if (isPlayerInside && butcherNative.GetComponent<LUD_NonDialogueReactions>().isArrivedToEast && !isShowWhereIsEllyaAppeared)
            {
                butcherNative.GetComponent<LUD_NonDialogueReactions>().ShowWhereIsEllya();
                isShowWhereIsEllyaAppeared = true;
                EndOfSceneTextTrigger.SetActive(true);


            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                isPlayerInside = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                isPlayerInside = false;
            }
        }
    }
}
