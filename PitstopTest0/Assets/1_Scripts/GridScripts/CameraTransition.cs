using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class CameraTransition : MonoBehaviour
    {
        [SerializeField] GameObject virtualCameraPlayer = default;
        [SerializeField] GameObject virtualCameraGlade = default;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                virtualCameraGlade.SetActive(true);
                virtualCameraPlayer.SetActive(false);
            }
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.tag == "Player")
            {
                virtualCameraGlade.SetActive(true);
                virtualCameraPlayer.SetActive(false);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                virtualCameraPlayer.SetActive(true);
                virtualCameraGlade.SetActive(false);
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