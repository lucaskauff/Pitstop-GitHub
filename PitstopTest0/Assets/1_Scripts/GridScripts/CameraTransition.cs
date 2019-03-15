using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class CameraTransition : MonoBehaviour
    {
        [SerializeField]
        GameObject virtualCameraPlayer;
        [SerializeField]
        GameObject virtualCameraGlade;

        void OnTriggerStay2D(Collider2D collider)
        {
            if (collider.gameObject.name == "Zayn")
            {
                virtualCameraGlade.SetActive(true);
                virtualCameraPlayer.SetActive(false);
            }
        }

        void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.gameObject.name == "Zayn")
            {
                virtualCameraPlayer.SetActive(true);
                virtualCameraGlade.SetActive(false);
            }
        }
    }
}