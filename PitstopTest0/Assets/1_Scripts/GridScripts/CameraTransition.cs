using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Pitstop
{
    public class CameraTransition : MonoBehaviour
    {
        [SerializeField] GameObject virtualCameraPlayer = default;
        [SerializeField] GameObject virtualCameraGlade = default;
        [SerializeField] Camera uiCamera = default;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                virtualCameraGlade.SetActive(true);
                virtualCameraPlayer.SetActive(false);

                
                uiCamera.orthographicSize = 11;
            }
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.tag == "Player")
            {
                virtualCameraGlade.SetActive(true);
                virtualCameraPlayer.SetActive(false);

               
                uiCamera.orthographicSize = 11;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                virtualCameraPlayer.SetActive(true);
                virtualCameraGlade.SetActive(false);

                
                uiCamera.orthographicSize = 5;
            }
        }

        void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.gameObject.tag == "Player")
            {
                virtualCameraPlayer.SetActive(true);
                virtualCameraGlade.SetActive(false);

                
                uiCamera.orthographicSize = 5;
            }
        }
    }
}