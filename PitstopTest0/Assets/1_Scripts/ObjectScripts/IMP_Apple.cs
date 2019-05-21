using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class IMP_Apple : MonoBehaviour
    {
        public float appleProjectionSpeed = 3;
        public bool hasExploded = false;
        [SerializeField] ScannableObjectBehaviour scannableObjectBehaviour = default;
        [SerializeField] Animator myAnim = default;
        [SerializeField] GameObject explosionRange = default;

        public AudioSource soundOfExplosion;
        public AudioSource soundWhileInTheAir;

        private void Start()
        {
            myAnim = GetComponent<Animator>();
        }

        private void Update()
        {
            if (scannableObjectBehaviour.isArrived)
            {
                ExplosionAnimStart();
            }
        }

        public void ExplosionAnimStart()
        {
            
            myAnim.SetTrigger("Explosion");
            
            hasExploded = true;
        }

        public void Explode()
        {
            soundWhileInTheAir.Stop();
            soundOfExplosion.Play();

            explosionRange.SetActive(true);
        }

        public void DestroyMe()
        {
            Destroy(gameObject);
        }
    }
}