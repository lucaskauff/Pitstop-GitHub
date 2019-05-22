using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class ParticleEffectDetection : MonoBehaviour
    {
        [SerializeField] ParticleSystem particleEffectAssociated = default;

        private AudioSource soundEffect;
        float maxVolume;

        private void Start()
        {
            soundEffect = FindObjectOfType<LUD_WayToFindLeavesSoundEffect>().gameObject.GetComponent<AudioSource>();
            maxVolume = soundEffect.volume;
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                particleEffectAssociated.Play();
                
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (collision.gameObject.GetComponent<PlayerControllerIso>().isMoving)
                {
                    StopAllCoroutines();
                    if (!soundEffect.isPlaying) soundEffect.Play();
                }
                else
                {
                    if (soundEffect.isPlaying) StartCoroutine(StopTheSoundEffectWithDelay());
                }

                
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                StartCoroutine(StopTheSoundEffectWithDelay());
            }
        }

        IEnumerator StopTheSoundEffectWithDelay()
        {
            

            for (int i = 0; i<5;i++)
            {
                yield return new WaitForSeconds(0.1f);
                soundEffect.volume = maxVolume -  maxVolume * (i/5f);
            }
            soundEffect.Stop();

            soundEffect.volume = maxVolume;
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