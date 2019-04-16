using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class IMP_Apple : MonoBehaviour
    {
        public float appleProjectionSpeed = 3;
        [SerializeField]
        ScannableObjectBehaviour scannableObjectBehaviour = default;
        [SerializeField]
        Animator myAnim = default;
        [SerializeField]
        GameObject explosionRange = default;

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
        }

        public void Explode()
        {
            explosionRange.SetActive(true);
        }

        public void DestroyMe()
        {
            Destroy(gameObject);
        }
    }
}