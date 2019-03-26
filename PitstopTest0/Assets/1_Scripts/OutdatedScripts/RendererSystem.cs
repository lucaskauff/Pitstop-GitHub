using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class RendererSystem : MonoBehaviour
    {
        [SerializeField]
        private float offsetValue = 0;

        [SerializeField]
        private bool runOnlyOnceForStaticObjects = false;

        void Update()
        {
            OrderingLayers();
        }

        public void OrderingLayers()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.y + offsetValue));

            if (runOnlyOnceForStaticObjects == true)
            {
                Destroy(this);
            }
        }
    }
}