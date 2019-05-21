using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Pitstop
{
    public class RockBehaviour : MonoBehaviour
    {        
        [Header("My Components")]
        [SerializeField] Animator myAnim = default;
        [SerializeField] Renderer myRend = default;
        [SerializeField] Rigidbody2D myRb = default;
        public Collider2D myCol = default;
        public Collider2D myTrigger = default;

        //Public
        public float heightWhereToSpawn;
        public float fallSpeed;
        public bool isOnRepulse = false;

        //Serializable
        [SerializeField] float impulseDuration = default;
        [SerializeField] GameObject rockDetection = default;
        [SerializeField] ScannableObjectBehaviour scannableObjectBehaviour = default;
        [SerializeField] CinemachineImpulseSource playerImpulseSource = default;
        [SerializeField] float repulseDelay = 1f;

        //Private
        private bool impulseGenerated = false;
        private bool arrivalCheck = false;
        private bool fallCheck = false;

        private void Start()
        {
            if (scannableObjectBehaviour.isFired)
            {
                myRend.enabled = false;
                myCol.enabled = false;
                myTrigger.enabled = false;
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

                //rockDetection.SetActive(false);
                myCol.enabled = false;
                myTrigger.enabled = false;
                return;
            }
            else
            {
                //rockDetection.SetActive(true);
                myCol.enabled = true;
                myTrigger.enabled = true;
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "ObjectApple")
            {
                StartCoroutine(WaitAfterBeingRepulsed());
            }
        }

        IEnumerator CameraShake()
        {
            playerImpulseSource.GenerateImpulse();
            yield return new WaitForSeconds(impulseDuration);
            impulseGenerated = true;
        }

        IEnumerator WaitAfterBeingRepulsed()
        {
            myRb.bodyType = RigidbodyType2D.Dynamic;
            myRb.gravityScale = 0;
            myCol.isTrigger = true;
            isOnRepulse = true;
            yield return new WaitForSeconds(repulseDelay);
            myRb.velocity = Vector2.zero;
            myRb.bodyType = RigidbodyType2D.Kinematic;
            myCol.isTrigger = false;
            isOnRepulse = false;
            StopCoroutine(WaitAfterBeingRepulsed());
        }
    }
}