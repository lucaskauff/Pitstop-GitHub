using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class ImpulseAppleBehaviour : MonoBehaviour
    {
        [SerializeField]
        float shockwaveLength = default;

        public ScannableObjectBehaviour scannableObjectBehaviour = default;
        public GameObject shockwave = default;

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