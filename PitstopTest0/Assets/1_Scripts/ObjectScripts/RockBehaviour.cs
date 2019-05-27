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
        public bool lianaRepulse = false;        
        public Vector2 newTarget;
        public float projSpeed = 1;
        public bool shouldGenerateImpulse = true;

        //Serializable
        [SerializeField] float impulseDuration = default;
        [SerializeField] ScannableObjectBehaviour scannableObjectBehaviour = default;
        [SerializeField] CinemachineImpulseSource playerImpulseSource = default;
        [SerializeField] float repulseDelay = 1f;
        [SerializeField] float newMass = 250f;
        [SerializeField] Transform cheatTransform = default;
        [SerializeField] float cheatSpeed = default;
        [SerializeField] bool isCheating = false;
        [SerializeField] string cheatingApple = null;

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

                myCol.enabled = false;
                myTrigger.enabled = false;
                return;
            }
            else
            {
                myCol.enabled = true;
                myTrigger.enabled = true;

                if (lianaRepulse)
                {
                    transform.position = Vector2.MoveTowards(transform.position, newTarget, projSpeed * Time.deltaTime);
                }
            }

            if (scannableObjectBehaviour.isArrived && !arrivalCheck)
            {
                RockApparition();
            }

            if (isCheating)
            {
                TheCheat();
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

            if (shouldGenerateImpulse)
            {
                StartCoroutine(CameraShake());
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "ObjectApple")
            {
                if (cheatTransform == null)
                {
                    StartCoroutine(WaitAfterBeingRepulsed());
                }
                else if (collision.gameObject.name == cheatingApple)
                {
                    isCheating = true;
                }
            }
        }

        private void TheCheat()
        {
            myCol.isTrigger = true;
            isOnRepulse = true;

            transform.position = Vector2.MoveTowards(transform.position, cheatTransform.transform.position, cheatSpeed * Time.deltaTime);

            if (transform.position == cheatTransform.transform.position)
            {
                myCol.isTrigger = false;
                isOnRepulse = false;
                isCheating = false;
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
            myRb.mass = newMass;
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