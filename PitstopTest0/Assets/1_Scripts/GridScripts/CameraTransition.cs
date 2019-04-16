using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class CameraTransition : MonoBehaviour
    {
        [SerializeField]
        GameObject virtualCameraPlayer = default;
        [SerializeField]
        GameObject virtualCameraGlade = default;

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.tag == "Player")
            {
                virtualCameraGlade.SetActive(true);
                virtualCameraPlayer.SetActive(false);
            }
        }

        void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.gameObject.tag == "Player")
            {
                virtualCameraPlayer.SetActive(true);
                virtualCameraGlade.SetActive(false);
            }
        }
    }
}