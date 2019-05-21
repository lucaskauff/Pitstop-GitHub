using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class CrystalDetectionScript : MonoBehaviour
    {
        [SerializeField] Animator myAnim = default;
        public bool isThereAScannableObject = false;

        private void Update()
        {
            myAnim.SetBool("ObjectThere", isThereAScannableObject);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<ScannableObjectBehaviour>() != null && collision.gameObject.GetComponent<ScannableObjectBehaviour>().isScannable)
            {
                isThereAScannableObject = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<ScannableObjectBehaviour>() != null && collision.gameObject.GetComponent<ScannableObjectBehaviour>().isScannable)
            {
                isThereAScannableObject = false;
            }
        }
    }
}