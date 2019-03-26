using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class ImpulseAppleBehaviour : MonoBehaviour
    {
        [SerializeField]
        float shockwaveLength;

        public ScannableObjectBehaviour scannableObjectBehaviour;
        public GameObject shockwave;

        private void Start()
        {
            shockwave.transform.localScale *= shockwaveLength;
        }

        private void Update()
        {
            if (scannableObjectBehaviour.isArrived)
            {
                shockwave.GetComponent<PolygonCollider2D>().enabled = true;
                //Destroy(gameObject);
            }
        }
    }
}