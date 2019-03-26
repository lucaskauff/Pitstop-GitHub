using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class IMP_Apple : MonoBehaviour
    {
        [SerializeField]
        ScannableObjectBehaviour scannableObjectBehaviour;
        public float appleProjectionSpeed;
        [SerializeField]
        GameObject explosionRange;
        [SerializeField]
        int timer;

        private void Update()
        {
            if (scannableObjectBehaviour.isArrived)
            {
                Explode();
                //StartCoroutine(Explosion());
            }
        }

        public void Explode()
        {
            explosionRange.SetActive(true);
            Destroy(gameObject, 0.1f);
        }

        IEnumerator Explosion()
        {
            yield return new WaitForSeconds(timer);
            explosionRange.SetActive(true);
            Destroy(gameObject, 0.1f);
        }
    }
}