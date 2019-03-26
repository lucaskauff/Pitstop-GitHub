using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Pitstop
{
    public class RockBehaviour : MonoBehaviour
    {
        //components
        Animator myAnim;
        Renderer myRend;

        public float heightWhereToSpawn;
        public float fallSpeed;

        public ScanData data;

        [SerializeField]
        float impulseDuration;
        [SerializeField]
        GameObject rockDetection;
        [SerializeField]
        ScannableObjectBehaviour scannableObjectBehaviour;
        [SerializeField]
        CinemachineImpulseSource playerImpulseSource;

        private bool impulseGenerated = false;
        private bool arrivalCheck = false;
        private bool fallCheck = false;

        private void Start()
        {
            myAnim = GetComponent<Animator>();
            myRend = GetComponent<Renderer>();

            if (scannableObjectBehaviour.isFired)
            {
                myRend.enabled = false;
            }
        }

        private void Update()
        {
            if (scannableObjectBehaviour.isFired)
            {
                if (!fallCheck)
                {
                    myAnim.SetTrigger("FallAnim");
                    myRend.enabled = true;
                    fallCheck = true;
                }

                rockDetection.SetActive(false);
                return;
            }
            else
            {
                rockDetection.SetActive(true);
            }

            if (scannableObjectBehaviour.isArrived && !arrivalCheck)
            {
                RockApparition();
            }
        }

        void RockApparition()
        {
            if (impulseGenerated)
            {
                StopCoroutine(CameraShake());
                arrivalCheck = true;
                return;
            }

            StartCoroutine(CameraShake());
        }

        IEnumerator CameraShake()
        {
            playerImpulseSource.GenerateImpulse();
            yield return new WaitForSeconds(impulseDuration);
            impulseGenerated = true;
        }
    }
}