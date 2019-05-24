using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class Glade3Menhir : MonoBehaviour
    {
        [SerializeField] Animator myAnim = default;
        [SerializeField] GameObject associatedHole = default;
        [SerializeField] Animator dust = default;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log(collision.gameObject.name);

            if (collision.gameObject.name == "AppleForThePlayerToScan2(Clone)")
            {
                myAnim.SetTrigger("MenhirIsTouched");
            }
        }

        public void BridgeIsFormed()
        {
            associatedHole.SetActive(false);
            dust.SetTrigger("Wush");
        }
    }
}