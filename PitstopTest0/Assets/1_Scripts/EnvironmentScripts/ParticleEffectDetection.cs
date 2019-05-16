using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class ParticleEffectDetection : MonoBehaviour
    {
        [SerializeField] ParticleSystem particleEffectAssociated = default;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                particleEffectAssociated.Play();
            }
        }

        /* Guess it's better without this
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                particleEffectAssociated.Stop();
            }
        }
        */
    }
}